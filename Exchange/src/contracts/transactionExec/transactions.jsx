import Caver from 'caver-js';
import Web3 from 'web3';

/** 
* smartcontract 함수 실행을 위함(send 함수를 사용함. call용에는 맞지 않음)
*
* @param {Array} _contractABI - Smart Contract ABI
* @param {string} _contractAddress - Smart Contract의 Address
* @param {string} _methods - 호출할 smart contract method 이름(ex. mint)
* @param {string} _wallet - "Metamask" 나 "Kaikas"
* @param {any} _args - Smart Contract에 전달할 인자
* @returns {Promise<void>}
* @warning Baobab-testnet에서 deploy한 contract는 metamask에서 실행 불가능. 이 프로젝트에서는 사용하지 않았음
* @example
* await contract Exectue(contractABI, contractAddress, mint, "Metamask", tokenUri);
**/
async function contractExecute(_contractABI, _contractAddress, _methods, _contract, _wallet, _args=null) {
    if (_wallet === "Metamask") {
        try {
            const web3 = new Web3(Web3.givenProvider);
            const accounts = await web3.eth.requestAccounts();
            const contract = new web3.eth.Contract(_contractABI, _contractAddress);
            if (_args !== undefined) {
                contract.methods[_methods](_args).send({
                    from: accounts[0],
                    gas:'2000000',
                });
            } else {
                contract.methods[_methods]().send({
                    from:accounts[0],
                    gas:'2000000',
                });
            }
        } catch(error) {
            console.log(error)
        }

    } else if (_wallet === "Kaikas") {
        const caver = new Caver(window.klaytn);
        try {
            const contract = new caver.klay.Contract(_contractABI, _contractAddress);
            if (_args !== undefined) {
                await contract.methods[_methods](_args).send({
                    from:window.klaytn.selectedAddress,
                    gas: '2000000',
                });
            } else {
                await contract.methods[_methods]().send({
                    from:window.klaytn.selectedAddress,
                    gas:'2000000',
                });
            }
        } catch(error) {
            console.log(error);
        }
    }
}

export default contractExecute;