## Contract Address
- Token : 0xee200efca30cc49871d21bfa109ec3dafd6925b3
- My NFT : 0x3bd19bf2e77f90ce4d1333cda8890e2a68c30da3
- MarcketPlace : 0xfabedaa6af05b6e3016a0eb62dfc8c0252297e0c

## How to run?
[env - server, for pinata api](https://github.com/Minkun00/gameExchange/blob/master/server/.env.example), [env - React, for Klaytn API](https://github.com/Minkun00/gameExchange/blob/master/src/.env.example)에 해당하는 내용 추가 후,
- 참고로, [env - React, for Klaytn API](https://github.com/Minkun00/gameExchange/blob/master/src/.env.example)이거 처음에 내가 할 때 root directory에 있어야 됬던거 같기도 한데 뭐 그정도는 실행하시는 분이 한 번만 수고 좀 부탁드리겠습니다..

```bash
user~/exchanges$ npm i
user~/exchanges$ cd server
user~/exchanges$ node server.js
user~/exchanges$ npm run start
```

## 초기 의도

게임에서 Item을 거래하는 것은 당연하다. 

하지만, 어떤 아이템들은 너무 고가라서 게임 상에서는 거래하지 않는 경우도 있다. 

이런 경우가 NFT가 필요한 경우가 아닐까?

그래서, 게임 아이템 코드 값을 입력하면 `server/server.js`의 API서버에서 `server/Pinata/Pinata.js`이미지를 생성하고, metadata와 함께 ipfs상에 저장한다.

그 정보를 바탕으로 자동으로 minting하게 코드를 작성하였다. 

유저는 민팅한 NFT를 가격을 정해서 거래소에 올린다. 이 방식은, 직접 이미지를 넣는 방식을 빼면 OpeanSea와 동일한 방식이다.

---

여기서 조금 다른 점을 생각했다.

nft를 구매하고, 구매한 상태에서 `아이템 코드`로 export 하면, 게임에 입력했을 때, 그 아이템을 반영하는 것이다.

간단하게 생각한 점이라 보안상에서는 문제가 있을 것이다. 나름의 기본적인 지식으로 생각한 것은, 우리는 계좌address로 구분하고, login느낌으로 생각하니, 

NFT를 응용하는 거래소를 사용하는 게임이면 Metamask, Kaikas를 연동할 것이다. 그 계좌 주소가 곧 id가 되는 것이고, 인증 방식에 id가 동일한지 확인하는 방법을

게임을 만들었을 때 추가한다면 상당히 재밌을 것 같다.

---

물론, 이 내용에 추가해서 따로 `klaytn API`를 사용하여 주소에 따라 가지고 있는 NFT의 그림을 모두 가져오는 작업도 진행하였다.

- smart contract는 **Token.sol**에 있다. remix에서 baobab testnet으로 deploy하여, abis들은 `src/Hardhat_abis/`에 있다.

## Server
위치 - `server/`

port - `3001`(react에서 3000쓰기에 3001로 했다)

게임 아이템의 코드를 입력하고, homepage에서 api호출을 하면 server에서 이미지를 생성하고 pinata를 통해 ipfs에 올린다.

그리고, 그 이미지의 ipfs url을 받아서 react화면 상에 표출한다. 자동적으로 민팅을 진행하게 된다.

이미지는 `server/Pinata/Images/`내에 저장되어 있으며, `Pinata.js`에 따라서 이미지를 가져올 수 있다.

- 참고로, 코드는 숫자 4자리만 입력하면 된다. 코드 보면서 적당히 때려넣으면 될듯? 사실 만들어놓고 테스트 할 떄 그냥 1234나 2222만 넣었다.

## react

react상의 코드와 관련해서는 [여기](https://github.com/Minkun00/gameExchange/tree/master/src)에서 확인할 수 있다. 여기에도 README.md를 써두겠다.

## config

`npm run eject`해가지고 생겼다. `buffer`를 react상에서 쓰고 싶었는데, 그거 때문에 고치다가 어쩔 수 없이 `eject`해서 그렇다. 그리고 자질구레한 오류들을 고치기 위해서..