// SPDX-License-Identifier: MIT
pragma solidity ^0.8.18;

import "@openzeppelin/contracts/access/Ownable.sol";
import "./1_Token.sol";
import "./2_NFT.sol";

/**
 * @dev NFT 거래용 Contract. Token을 재화로 사용함
 */
contract MyMarketplace is Ownable {
    MyNFT public nft;
    MyToken public token;
    uint256 public feePercentage = 10;      // 판매 10% 수수료

    /**
     * @dev toeknId => market에 올라가 있는지 확인용
     */
    mapping(uint256 => bool) public isListed;

    /**
     * @dev tokenId => price
     */
    mapping(uint256 => uint256) public nftPrices;

    /**
     * @dev tokenId => 판매자. 판매대금 전달용
     */
    mapping(uint256 => address) private _sellers;


    event NFTListed(uint256 tokenId, address seller);
    event NFTSold(uint256 tokenId, uint256 price, address buyer);

    constructor(address _nft, address _token) {
        nft = MyNFT(_nft);
        token = MyToken(_token);
    }

    /**
     * @dev NFT를 Market에 등록
     * @param tokenId 소유하는 tokenId
     * @param price 판매 가격 설정
     */
    function listNFT(uint256 tokenId, uint256 price) public {
        require(nft.ownerOf(tokenId) == msg.sender, "Not the owner");
        require(!isListed[tokenId], "Already listed");
        require(price > 0, "Price must be greater than zero");

        nft.transferFrom(msg.sender, address(this), tokenId);
        isListed[tokenId] = true;
        nftPrices[tokenId] = price;
        _sellers[tokenId] = msg.sender;
        emit NFTListed(tokenId, msg.sender);
    }

    /**
     * @dev market에 등록된 NFT구매
     * @param tokenId market에 등록된 NFT의 tokenId값
     */
    function purchaseNFT(uint256 tokenId) external payable {
        require(isListed[tokenId], "Not listed");
        uint256 price = nftPrices[tokenId];
        require(token.allowance(msg.sender, address(this)) >= price, "token allowance not enough");

        uint256 fee = (price * feePercentage) / 100;
        uint256 sellerProceeds = price - fee;
        address seller = _sellers[tokenId];

        require(token.transferFrom(msg.sender, owner(), fee), "Fee transfer failed");
        require(token.transferFrom(msg.sender, seller, sellerProceeds), "Seller proceeds transfer failed");
        nft.transferFrom(address(this), msg.sender, tokenId);
        isListed[tokenId] = false;
        nftPrices[tokenId] = 0;
        delete _sellers[tokenId];

        emit NFTSold(tokenId, price, msg.sender);
    }

    /**
     * @dev market에 올라간 NFT 정보 제공
     * @return tokenIds NFT의 tokenId 배열
     * @return prices NFT의 가격 배열
     */
    function getListedNFTs() public view returns (uint256[] memory, uint256[] memory) {
        uint256 itemCount = nft.getCurrentTokenId(); // 수정된 부분 
        uint256 listedCount = 0; 

        // 리스트된 NFT의 수를 세기 위한 루프
        for (uint256 i = 0; i < itemCount; i++) {
            if (isListed[i + 1]) { 
                listedCount += 1;
            }
        }


        uint256[] memory tokenIds = new uint256[](listedCount);
        uint256[] memory prices = new uint256[](listedCount);

        // 리스트된 NFT의 정보를 수집하는 루프
        uint256 currentIndex = 0;
        for (uint256 i = 0; i < itemCount; i++) {
            if (isListed[i + 1]) {
                uint256 currentId = i + 1;
                tokenIds[currentIndex] = currentId;
                prices[currentIndex] = nftPrices[currentId]; 
                currentIndex += 1;
            }
        }
        return (tokenIds, prices);
    }    
}