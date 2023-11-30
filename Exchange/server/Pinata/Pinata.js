require("dotenv").config();
const itemABI = require('./itemCode.json').Code;

const axios = require("axios");
const FormData = require("form-data");
const sharp = require("sharp");
const { Readable } = require('stream');
const baseUrl = "https://gateway.pinata.cloud/ipfs/";
const { ACCESS_KEY, SECRET_ACCESS_KEY } = process.env;

const usePinata = async (code, _name, _description) => {   
  // code : item code, _name : name of the item, _description : description of the item
  console.log(`got code : ${code}`);
  // console.log("itemABI : ", itemABI);

  const codeObj = await disolveCode(code);
  const _itemName = `[${codeObj.rank}] ${codeObj.adjective} ${codeObj.itemName} ${codeObj.item}`;
  console.log(`itemName : ${_itemName}`);
  if (Object.values(codeObj).includes('Unknown')) {
    console.log(`!ERROR! : Unknown code`);
    return { error: "Unknown code" };
  }
  

  const combinedImageBuffer = await combineImages(codeObj);


  const imageStream = new Readable({
    read() {
      this.push(combinedImageBuffer);
      this.push(null);
    }
  });
  
  const formData = new FormData();
  formData.append("file", imageStream, {
    filename: `${code}.png`
  });

  const imgUrl = await uploadImgToPinata(formData);
  console.log("Pinata에 이미지 저장이 완료되었습니다. : ", imgUrl);

  const metaData = {
    name: _itemName,
    description: _description,
    image: imgUrl,
    code: `${code}`
  };

  const tokenUri = await jsonToPinata(metaData);
  console.log("Pinata에 메타데이터 저장이 완료되었습니다. : ", tokenUri);
  console.log("---------------------------")
  return { imgUrl, tokenUri, metaData };
};

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

const getCodeValue = (codeMap, code) => {
  const foundKey = Object.keys(codeMap).find(key => codeMap[key] === code);
  return foundKey !== undefined ? foundKey : 'Unknown';
}


const combineImages = async (codeObj) => {
  const backgroundPath = `./Pinata/Images/Background/${codeObj.background}.png`;
  const itemPath = `./Pinata/Images/${codeObj.itemName}/${codeObj.item}.png`;
  const rankEdgePath = `./Pinata/Images/RankEdge/${codeObj.rank}.png`;

  let combinedImage = sharp(backgroundPath);

  combinedImage = combinedImage.composite([{ input: rankEdgePath }]);

  combinedImage = combinedImage.composite([{ input: itemPath }]);

  const outputBuffer = await combinedImage.toBuffer();
  return outputBuffer;
};


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