require("dotenv").config();
const itemABI = require('./itemCode.json').Code;  // img는 json으로 code값 기반으로 분류
const cryption = require('./hash'); // en/decrypt javascript code
const axios = require("axios");
const FormData = require("form-data");
const fs = require("fs");
const sharp = require("sharp");
const { Readable } = require('stream');
const baseUrl = "https://gateway.pinata.cloud/ipfs/";
const { ACCESS_KEY, SECRET_ACCESS_KEY } = process.env;

/**
 * @description Main function
 * @param {String} code item code
 * @param {String} action encrypt, decrypt
 * @returns 
 */
const usePinata = async (code, action) => {   
  console.log(`got code : ${code} | mode : ${action}`);

  if (action == 'decrypt') {
    const decryptedCode = cryption.decryptCode(code);
    console.log(`decryptedCode : ${decryptedCode}`);
    console.log('-------------------------');
    return {decryptedCode};
  }

  // code 분해 -> 이미지 합성을 위함
  const codeObj = await disolveCode(code);

  const _itemName = `[${codeObj.rank}] ${codeObj.adjective} ${codeObj.itemName} ${codeObj.item}`;
  console.log(`itemName : ${_itemName}`);

  // code값 오류 발생 시 error 발생
  if (Object.values(codeObj).includes('Unknown')) {
    console.log(`!ERROR! : Unknown code`);
    return { error: "Unknown code" };
  }
  
  // code에 따라 image 합성과정(type : Buffer)
  const combinedImageBuffer = await combineImages(codeObj);

  // 합성된 Buffer -> Image
  const imageStream = new Readable({
    read() {
      this.push(combinedImageBuffer);
      this.push(null);
    }
  });
  
  // Pinata 사용을 위한 image file로 생성
  const formData = new FormData();
  formData.append("file", imageStream, {
    filename: `${code}.png`
  });

  // Pinata 사용해서 image를 ipfs에 upload
  const imgUrl = await uploadImgToPinata(formData);
  console.log("Pinata에 이미지 저장이 완료되었습니다. : ", imgUrl);

  const encryptedCode = cryption.encryptCode(code);
  // metadata 작성
  const metaData = {
    name: _itemName,
    image: imgUrl,
    code: `${encryptedCode}`
  };

  // metadata도 Pinata를 통해 ipfs에 upload
  const tokenUri = await jsonToPinata(metaData);
  console.log("Pinata에 메타데이터 저장이 완료되었습니다. : ", tokenUri);
  console.log("---------------------------")
  return { imgUrl, tokenUri };
};

/**
 * @description Image 합성을 위한 code 분해과정. itemCode.json에 따라 분해
 * @param {String} code item code 
 * @returns {Object} code 분해
 * @see getCodeValue
 */
const disolveCode = async (code) => {
  const codeLength = 17;
  const codeString = code.padStart(17, '0'); 
  if (codeString.length !== codeLength) {
    console.log("!ERROR! : Invalid code length");
    return { error: "Invalid code length" };
  }

  const adjectiveCode = codeString.slice(0, 3);
  const itemNameCode = codeString.slice(3, 5);
  const itemCode = codeString.slice(5, 8);
  const backgroundCode = codeString.slice(8, 14);
  const rankCode = codeString.slice(14, 17);

  let codeObj = {
    adjective: getCodeValue(itemABI.Adjective, adjectiveCode),
    background: getCodeValue(itemABI.Color, backgroundCode),
    itemName: getCodeValue(itemABI.ItemName, itemNameCode),
    item: getCodeValue(itemABI.Item, itemCode),
    rank: getCodeValue(itemABI.Rank, rankCode)
  };

  return codeObj;
};

/**
 * @description code의 명칭 찾기
 * @param {JSON} codeMap 부분에 따른 JSON의 내부 값 
 * @param {String} code codeMap에서 찾을 내용 
 * @returns {String} code에 따른 명칭, 찾지 못하면 'Unknown'
 */
const getCodeValue = (codeMap, code) => {
  const foundKey = Object.keys(codeMap).find(key => codeMap[key] === code);
  return foundKey !== undefined ? foundKey : 'Unknown';
}

/**
 * @description item 명칭에 따라 image 합성과정
 * @param {Object} codeObj 
 * @returns {Buffer} image Buffer
 * @see checkImagesTest
 */
const combineImages = async (codeObj) => {
  const backgroundPath = `./Pinata/Images/Background/${codeObj.background}.png`;
  const itemPath = `./Pinata/Images/${codeObj.itemName}/${codeObj.item}.png`;
  const rankEdgePath = `./Pinata/Images/RankEdge/${codeObj.rank}.png`;

  // image 합성과정
  // 1. Background
  let combinedImage = sharp(backgroundPath);
  combinedImage = await checkImagesTest(combinedImage, 1);
  // 2. rankEdge
  let combinedImageEdge = combinedImage.composite([{ input: rankEdgePath }]);
  combinedImageEdge = await checkImagesTest(combinedImageEdge, 2);
  // 3. item
  let combinedImageFin = combinedImageEdge.composite([{ input: itemPath }]);
  combinedImageFin = await checkImagesTest(combinedImageFin, 3);
  // 완성된 Buffer return
  const outputBuffer = await combinedImageFin.toBuffer();
  return outputBuffer;
};

/**
 * @param {Buffer} imageBuffer 
 * @param {Number} _int 
 * @returns {sharp} image합성을 위해 sharp형식으로 다시 생성
 */
const checkImagesTest = async (imageBuffer, _int) => {
  const outputBuffer = await imageBuffer.toBuffer();
  // fs.writeFileSync(`./combinedImage_${_int}.png`, outputBuffer);
  return sharp(outputBuffer);
};


/**
 * @description Pinata를 이용한 image ipfs에 업로드
 * @param {ImageBitmap} data 전송
 * @returns {URL} image ipfs url
 */
const uploadImgToPinata = async (data) => {
  try {
    const response = await axios.post("https://api.pinata.cloud/pinning/pinFileToIPFS", data, {
      maxContentLength: "Infinity",
      headers: {
        pinata_api_key: ACCESS_KEY,
        pinata_secret_api_key: SECRET_ACCESS_KEY,
      },
    });
    return `ipfs://${response.data.IpfsHash}`;
  } catch (err) {
    console.error("Error uploading image to Pinata:", err);
    throw err;
  }
};

/**
 * @description Pinata를 이용한 metadata ipfs에 업로드
 * @param {JSON} metaData NFT의 metadata
 * @returns {URL} metadata ipfs url
 */
const jsonToPinata = async (metaData) => {
  try {
    const response = await axios.post("https://api.pinata.cloud/pinning/pinJSONToIPFS", JSON.stringify(metaData), {
      headers: {
        "Content-Type": "application/json",
        pinata_api_key: ACCESS_KEY,
        pinata_secret_api_key: SECRET_ACCESS_KEY,
      },
    });
    return `${baseUrl}${response.data.IpfsHash}`;
  } catch (err) {
    console.error("Error saving metadata to Pinata:", err);
    throw err; 
  }
};

module.exports = usePinata;