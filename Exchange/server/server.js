const express = require('express');
const cors = require('cors');
const path = require('path');
const usePinata = require('./Pinata/Pinata');

require('dotenv').config({ path: path.resolve(__dirname, '../src/Pinata/.env') });

const app = express();

app.use(cors());
app.use(express.json()); // Body-parser is included in express

// 다른 라우트를 여기에 추가할 수 있습니다.

const PORT = process.env.PORT || 3001; // React 기본 포트인 3000과 충돌을 피하기 위해
app.listen(PORT, () => {
  console.log(`Server listening on port ${PORT}`);
  console.log(`directory : ${__dirname}`)
});



// POST 엔드포인트로 Pinata 기능을 통합
app.post('/api/pinata', async (req, res) => {
  const { code, name, description } = req.body;
  try {
    const { imgUrl, tokenUri, metaData } = await usePinata(code, name, description);
    res.json({ imgUrl, tokenUri, metaData });
  } catch (error) {
    res.status(500).send({ error : error.message });
  }
});
