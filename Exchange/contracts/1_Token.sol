// SPDX-License-Identifier: MIT
pragma solidity ^0.8.18;

import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
/**
 * @dev ERC20을 활용한 Token발행. Marketplace에서 사용할 재화
 */
contract MyToken is ERC20 {
    constructor(uint256 initialSupply) ERC20("MyToken", "MTK") {
        _mint(msg.sender, initialSupply);
    }

    /**
     * @dev klay로 token구매. 1:1 비율로 실행
     */
    function purchase() external payable {
        uint256 amountToMint = msg.value; 
        _mint(msg.sender, amountToMint);
    }
}
