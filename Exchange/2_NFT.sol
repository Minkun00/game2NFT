// SPDX-License-Identifier: MIT
pragma solidity ^0.8.18;

import "@openzeppelin/contracts/token/ERC721/extensions/ERC721Enumerable.sol";
import "@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/utils/Counters.sol";
/**
 * @dev NFT mint, burn, Market에 대한 권한 위임등 처리.
 */
contract MyNFT is ERC721URIStorage, ERC721Enumerable, Ownable {
    using Counters for Counters.Counter;
    Counters.Counter private _tokenIdCounter;

    /**
     * @dev Market에게 msg.sender의 NFT 권한 위임 확인용
     */
    mapping(address => bool) private _marketplaceApprovals;

    constructor(string memory name, string memory symbol) ERC721(name, symbol) {}
    
    /**
     * @dev Market에 권한 위임
     * @param marketplace marketplace contract 주소
     * @param approved React에서 true로 실행함
     */
    function setMarketplaceApproval(address marketplace, bool approved) external {
        setApprovalForAll(marketplace, approved);
        _marketplaceApprovals[msg.sender] = true;
    }

    /**
     * @dev Market에 위임했는지 확인
     */
    function isMarketplaceApproved() external view returns (bool) {
        return _marketplaceApprovals[msg.sender];
    }

    /**
     * @dev ERC721Enumerable를 사용할 때, 부모 contract override 필요
     */
    function _beforeTokenTransfer(address from, address to, uint256 tokenId)
        internal
        override(ERC721, ERC721Enumerable)
    {
        super._beforeTokenTransfer(from, to, tokenId);
    }
    /**
     * @dev ERC721사용을 위한 구현. 따로 아래에 burn함수를 만들어서 사용함.
     */
    function _burn(uint256 tokenId) internal override(ERC721, ERC721URIStorage) {
        super._burn(tokenId);
    }

    /**
     * @dev 토큰 URI를 가져오기 위한 함수
     * @param tokenId NFT의 tokenId
     */ 
    function tokenURI(uint256 tokenId)
        public
        view
        override(ERC721, ERC721URIStorage)
        returns (string memory)
    {
        return super.tokenURI(tokenId);
    }

    /**
     * @dev 모든 부모 컨트랙트에 구현된 supportsInterface 함수를 호출
     */
    function supportsInterface(bytes4 interfaceId)
        public
        view
        override(ERC721, ERC721Enumerable)
        returns (bool)
    {
        return super.supportsInterface(interfaceId);
    }

    /**
     * @dev NFT를 민팅하고, 토큰 URI를 설정. msg.sender가 NFT의 주인이 됨
     */
    function mint(string memory uri) public {
        _tokenIdCounter.increment();
        uint256 newItemId = _tokenIdCounter.current();
        _safeMint(msg.sender, newItemId);
        _setTokenURI(newItemId, uri); // 토큰 ID와 메타데이터 URI를 연결
    }

    /**
     * @dev 본인 소유의 NFT 삭제. 다시 게임으로 돌려보내기 위해 React에서 코드로 제출
     * @param tokenId NFT의 tokenId
     */
    function burn(uint256 tokenId) public returns (bool) {
        address owner = ownerOf(tokenId);
        require(owner == msg.sender, "You are not the owner of this token");

        require(_exists(tokenId), "Token does not exist");
        _burn(tokenId);
        return true;
    }

    /**
     * @dev 지금까지 발행된 token이 총 몇개인지 확인용. 삭제된 것과 관계없이 mint하면 한 개씩 증가하기에 확인용도임.
     */
    function getCurrentTokenId() public view returns (uint256) {
        return _tokenIdCounter.current();   
    }
}