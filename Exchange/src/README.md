## REACT

이 폴더에서 react관련한 코드들이 존재한다.

### Hardhat_abis
remix에서 compile된 json파일 있는 곳.

### src/buyitem
- [marcket.jsx](https://github.com/Minkun00/gameExchange/blob/master/src/src/buyItem/marcket.jsx) : 거래소에 올라온 아이템들 확인 및 구매 절차.

### src/createAuction
- [createAuction.jsx](https://github.com/Minkun00/gameExchange/blob/master/src/src/createAuction/createAuction.jsx) : 이 contract 내에서 nft 발급한 것들을 모두 찾아오고, 가격 정해서 팔기

### src/itemToIMG
- [itemToImg.jsx](https://github.com/Minkun00/gameExchange/blob/master/src/src/itemToIMG/itemToImg.jsx) : `item code`, 추가 부여하는 text 2개 입력하면 api 호출을 통해서 이미지 생성, 생성 완료되면 자동 민팅 진행
- [useImageGenerator.jsx](https://github.com/Minkun00/gameExchange/blob/master/src/src/itemToIMG/useImageGenerator.jsx) : `itemToImg.jsx`에서 api호출하는 기능을 여기서 진행한다. 이 과정은 [https://github.com/Minkun00/gameExchange/tree/master/README.md/Server] 확인.

### src/kaikasConnect
- [KaikasConnect.jsx](https://github.com/Minkun00/gameExchange/blob/master/src/src/kaikasConnect/KaikasConnect.jsx) : `kaikas`지갑 연결. address를 가져온다. app.jsx에서 버튼 누르면 연결 진행함.
- [BuyTokens/BuyTokens.jsx](https://github.com/Minkun00/gameExchange/blob/master/src/src/kaikasConnect/BuyTokens/BuyTokens.jsx) : 내가 만든 토큰을 살 수 있게 하는 버튼 작동. 일단은 버튼 한번 누르면 5 klay 빠지고, 5 token을 주게 했다.