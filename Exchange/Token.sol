// SPDX-License-Identifier: MIT
pragma solidity ^0.8.18;

import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721Enumerable.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/utils/Counters.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";

contract MyToken is ERC20 {
    constructor(uint256 initialSupply) ERC20("MyToken", "MTK") {
        _mint(msg.sender, initialSupply);
    }

    function purchase() external payable {
        uint256 amountToMint = msg.value; // 1:1 비율
        _mint(msg.sender, amountToMint);
    }
}

contract MyNFT is ERC721URIStorage, ERC721Enumerable, Ownable {
    using Counters for Counters.Counter;
    Counters.Counter private _tokenIdCounter;

    constructor(string memory name, string memory symbol) ERC721(name, symbol) {}
    
    function setMarketplaceApproval(address marketplace, bool approved) external {
        setApprovalForAll(marketplace, approved);
    }

    // ERC721Enumerable를 사용할 때는 부모 컨트랙트의 함수를 오버라이드해야 합니다.
    function _beforeTokenTransfer(address from, address to, uint256 tokenId)
        internal
        override(ERC721, ERC721Enumerable)
    {
        super._beforeTokenTransfer(from, to, tokenId);
    }

    // 토큰 ID에 대한 정보를 얻기 위한 지원 함수입니다.
    function _burn(uint256 tokenId) internal override(ERC721, ERC721URIStorage) {
        super._burn(tokenId);
    }

    // 토큰 URI를 가져오기 위한 함수입니다.
    function tokenURI(uint256 tokenId)
        public
        view
        override(ERC721, ERC721URIStorage)
        returns (string memory)
    {
        return super.tokenURI(tokenId);
    }

    // 모든 부모 컨트랙트에 구현된 supportsInterface 함수를 호출합니다.
    function supportsInterface(bytes4 interfaceId)
        public
        view
        override(ERC721, ERC721Enumerable)
        returns (bool)
    {
        return super.supportsInterface(interfaceId);
    }

    // NFT를 민팅하고, 토큰 URI를 설정하는 함수입니다. msg.sender가 주인이 됩니다
    function mint(string memory uri) public {
        _tokenIdCounter.increment();
        uint256 newItemId = _tokenIdCounter.current();
        _safeMint(msg.sender, newItemId);
        _setTokenURI(newItemId, uri); // 토큰 ID와 메타데이터 URI를 연결합니다.
    }

    function burn(uint256 tokenId) public onlyOwner returns (bool) {
        require(_exists(tokenId), "Token does not exist");
        _burn(tokenId);
        return true;
    }
    // MyNFT 컨트랙트에 추가
    function getCurrentTokenId() public view returns (uint256) {
        return _tokenIdCounter.current();   
    }
}

contract MyMarketplace is Ownable {
    MyNFT public nft;
    MyToken public token;
    uint256 public feePercentage = 10;      // 10% 수수료

    mapping(uint256 => bool) public isListed;
    mapping(uint256 => uint256) public nftPrices;
    mapping(uint256 => address) private _sellers;


    event NFTListed(uint256 tokenId, address seller);
    event NFTSold(uint256 tokenId, uint256 price, address buyer);

    constructor(address _nft, address _token) {
        nft = MyNFT(_nft);
        token = MyToken(_token);
    }


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






