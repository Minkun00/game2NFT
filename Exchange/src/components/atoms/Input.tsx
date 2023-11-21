// Input.tsx (Atoms)

import React from 'react';
import styled from 'styled-components';

interface InputProps {
  type: string;
  placeholder: string;
  value: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

const StyledInput = styled.input`
  margin-bottom: 10px;
  padding: 5px;
  border: 1px solid #ddd;
  border-radius: 3px;
`;

const Input: React.FC<InputProps> = ({ type, placeholder, value, onChange }) => (
  <StyledInput type={type} placeholder={placeholder} value={value} onChange={onChange} />
);

export default Input;
