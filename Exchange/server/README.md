## SERVER
get `Code` of item to generate `image`, `metadata`, `tokenUri` for NFT Minting.

Code of this process is in `Pinata/Pinata.js`.

Port number is `3001`, not disturbing `React`, which is `3000`.

- made by javascript, express.

## HOW TO ACTIVATE

make `.env` file
```bash
ACCESS_KEY=<Pinata Access Key>
SECRET_ACCESS_KEY=<Pinata Secret Access Key>
NFT_CONTRACT_ADDRESS=<Address of NFT Contract : CA>
```

```bash
cd Exchange/server
node server.js
```

you can see 
```bash
Server listening on port 3001
directory : {~/server}
```
