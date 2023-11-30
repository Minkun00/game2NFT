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

## Item Code

| Name       | Code | Name    | Code | Name   | Code | Name      | Code | Name   | Code |
|------------|------|---------|------|--------|------|-----------|------|--------|------|
| 낭만있는    | 101  | Army    | 30   | Helmet | 100  | Red       | FF0000 | Normal | 123 |
| 쾌속의      | 202  | Knight  | 40   | Top    | 200  | Orange    | FF5E00 | Epic | 234 |
| 벽력일섬의  | 303  | Absolute| 50   | Pants  | 300  | Yellow    | FFE400 | Unique | 345 | 
| 음주가무의  | 404  |         |      | Shoes  | 400  | Green     | 1DDB16 | Legendary | 456|
| 불굴의      | 505  |         |      | Sword  | 500  | Skyblue   | 00D8FF |
| 진격의      | 606  |         |      |        |      | Blue      | 0100FF |
|            |      |         |      |        |      | Purple    | 3F0099 |


### example 
```bash
낭만있는 ArmyHelmet (붉은 배경, 노말 등급) = 10130100FF0000123
```
-----
if `server` gets `Code`, `Description` and `Name`, you can see `ipfs url` on terminal
