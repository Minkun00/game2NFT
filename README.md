# 의도

RPG 게임 등, 아이템의 중요도가 큰 게임에서는 거래소가 필수불가결한 요소이다. MMORPG게임에서는 거래소에서 거래되는 게임 아이템이 현실 돈의 가치를 지니는 경우도 많다. 물론, 적지 않은 양이기에 보안적인 면을 상당히 투자하는 부분일 것이다.

보안적인 부분에서 이견이 없는 Blockchain기술, smart contract를 사용하면 item 거래에서 상당히 큰 의미가 있다고 생각했다. 게임 아이템을 실제로 돈의 가치라고 인정할 수 있는 시대가 도래할 것이라고 생각한다. 그 흐름에 편승하기 위해서는 게임 아이템을 NFT로 만들어서 [OpenSea](https://opensea.io/kr)처럼 만들어보겠다는 생각을 했다.

추가적으로, P2E적인 게임을 지향하기에 Token 발급 및 환율 제도를 추가할 생각이다.

# 내용

## [Exchange](https://github.com/Minkun00/game2NFT/tree/main/Exchange)

- 목표

Game에서 직접 연결(API 등)하여 거래소 사용. Token을 사용하여 거래하기 때문에 Defi, GameFi적인 내용 추가.

- 11.5 
1. game item code 입력받으면 Pinata를 통해 NFT생성

2. demo version 게임 아이템 거래소 사이트 완성