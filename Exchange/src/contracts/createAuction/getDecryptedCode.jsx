import axios from 'axios';
import { useState, useCallback } from 'react';

export default function decryptCode() {
    const [decryptedCode, setDecryptedCode] = useState('');

    const decryption = useCallback(async (code) => {
        try {
            const action = 'decrypt';
            const response = await axios.post('http://localhost:3001/api/pinata', {code, action});
            const { decryptedCode } = response.data;
            setDecryptedCode(decryptedCode);
            return decryptedCode;
        } catch (error) {
            console.error('Error during decryption : ', error);
            return '';
        }
    }, [])

    return { decryptedCode, decryption };
}