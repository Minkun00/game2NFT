require("dotenv").config();

const axios = require("axios");
const FormData = require("form-data");
const sharp = require("sharp");
const { Readable } = require('stream');
const baseUrl = "https://gateway.pinata.cloud/ipfs/";
const { ACCESS_KEY, SECRET_ACCESS_KEY } = process.env;

const usePinata = async (code, _name, _description) => {   // code : item code, _name : name of the item, _description : description of the item
  const codeObj = await disolveCode(code);
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

  // 메타데이터
  const metaData = {
    name: _name,
    description: _description,
    image: imgUrl,
    code: `${code}`
  };

  const tokenUri = await jsonToPinata(metaData);
  console.log("Pinata에 메타데이터 저장이 완료되었습니다. : ", tokenUri);

  return { imgUrl, tokenUri, metaData };
};

const disolveCode = async(code) => {
  const codeString = String(code).padStart(4, '0'); 
  
  const backgroundCode = parseInt(codeString[0], 10);
  const itemNameCode = parseInt(codeString[1], 10);
  const itemCode = parseInt(codeString[2], 10);
  const rankCode = parseInt(codeString[3], 10);

  let codeObj = {
      background: getBackgroundByCode(backgroundCode),
      itemName: getItemNameByCode(itemNameCode),
      item: getItemByCode(itemCode),
      rank: getRankByCode(rankCode)
  };

  return codeObj;
}

const getBackgroundByCode = (code) => {
  switch (code) {
      case 1:
        return 'Blue';
      case 2:
        return 'Green';
      case 3:
        return 'Orange';
      case 4:
        return 'Purple';
      case 5:
        return 'Red';
      case 6:
        return 'Skyblue'; 
      default:
          return 'Yellow';
  }
}

const getItemNameByCode = (code) => {
  switch (code) {
      case 1:
        return 'Absolute';
      case 2:
        return 'Knight';
      default:
        return 'Default';
  }
}

const getItemByCode = (code) => {
  // 예시로 아래와 같이 설정, 실제 값에 맞게 수정 필요
  switch (code) {
    case 1:
      return 'Armor';
    case 2:
      return 'Helmet';
    case 3:
      return 'Pants';
    case 4:
      return 'Shoes';
    default:
      return 'Sword';
}
}

const getRankByCode = (code) => {
  // 예시로 아래와 같이 설정, 실제 값에 맞게 수정 필요
  switch (code) {
      case 1:
        return 'Normal';
      case 2:
        return 'Epic';
      case 3:
        return 'Unique';
      case 4:
        return 'Legendary';
      default:
        return 'Normal';
  }
}


const combineImages = async (codeObj) => {
  const backgroundPath = `./Pinata/Images/Background/${codeObj.background}.png`;
  const itemPath = `./Pinata/Images/${codeObj.itemName}/${codeObj.item}.png`;
  const rankEdgePath = `./Pinata/Images/RankEdge/${codeObj.rank}.png`;

  // Start with the background image
  let combinedImage = sharp(backgroundPath);

  // Overlay the rank edge image
  combinedImage = combinedImage.composite([{ input: rankEdgePath }]);

  // Overlay the item image
  combinedImage = combinedImage.composite([{ input: itemPath }]);

  // Finalize the composite and convert to a Buffer
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
    throw err; // 오류를 호출자에게 던져주어야 합니다.
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
    throw err; // 오류를 호출자에게 던져주어야 합니다.
  }
};


module.exports = usePinata;