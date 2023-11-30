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

<div style="display: flex; justify-content: space-between;">

  <div style="flex: 1; margin-right: 5px;">

  | Name       | Code |
  |------------|------|
  | 낭만있는    |  101 | 
  | 쾌속의      | 202  |
  | 벽력일섬의  | 303  |
  | 음주가무의  |  404  |
  | 불굴의      | 505  |
  | 진격의      | 606  |

  </div>

  <div style="flex: 1; margin-right: 5px;">

  | Name    | Code |
  |---------|------|
  | Army    | 30   |
  | Knight  | 40   |
  | Absolute| 50   |

  </div>

  <div style="flex: 1; margin-right: 5px;">

  | Name    | Code |
  |---------|------|
  | Helpmet | 100  |
  | Top     | 200  |
  | Pants   | 300  |
  | Shoes   | 400  |
  | Sword   | 500  |

  </div>
  
  <div style="flex: 1; margin-right: 5px;">
  
  | Name    | Code   |
  |---------|--------|
  | Red     | FF0000 |
  | Orange  | FF5E00 |
  | Yellow  | FFE400 |
  | Green   | 1DDB16 |
  | Skyblue | 00D8FF |
  | Blue    | 0100FF |
  | Purple  | 3F0099 |

  </div>

  <div style="flex: 1; margin-right: 5px;">

  | Name      | Code |
  |-----------|------|
  | Normal    | 123  |
  | Epic      | 234  |
  | Unique    | 345  |
  | Legendary | 456  |

  </div>

</div>

### example 
```bash
낭만있는 ArmyHelmet (붉은 배경, 노말 등급) = 10130100FF0000123
```