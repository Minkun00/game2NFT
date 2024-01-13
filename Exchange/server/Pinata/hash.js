const CryptoJS = require('crypto-js');
require('dotenv').config()
const { PARSE_KEY } = process.env;

/**
 * @description code 암호화
 * @param {String} code default code value
 * @returns {String} encrypted code
 * @throws {null}
 */
function encryptCode(code) {
    try {
        if (!PARSE_KEY) {
            console.log('No PARSE KEY !')
            return null;
        }
        const encrypted = CryptoJS.AES.encrypt(code, PARSE_KEY).toString();
        return encrypted
    } catch (e) {
        console.log('Encryption error occur : ', e);
        return null;
    }
}

/**
 * @description code 복호화
 * @param {String} code encrypted code 
 * @returns {String} original code
 * @throws {null}
 */
function decryptCode(code) {
    try {
        if (!PARSE_KEY) {
            console.log('No PARSE KEY !')
            return null;
        }
        const decrypted_bytes = CryptoJS.AES.decrypt(code, PARSE_KEY);
        const decrypted = decrypted_bytes.toString(CryptoJS.enc.Utf8);
        return decrypted
    } catch (e) {
        console.log('Decryption error occur : ', e);
        return null;
    }
}

module.exports = {
    encryptCode, decryptCode
};

if (require.main == module) {
    const code = 'testcode';
    const encryption = encryptCode(code)
    console.log(encryption);

    const decryption = decryptCode(encryption);
    console.log(decryption);
}
