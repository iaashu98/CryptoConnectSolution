export interface ICryptoIdSelector {
    selectedCryptoIds: string[];
    onCryptoIdChange: (cryptoIds: string[]) => void;
  }