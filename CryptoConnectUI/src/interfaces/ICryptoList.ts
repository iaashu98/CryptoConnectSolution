import { ICryptoMarketData } from "./ICryptoMarketData";

export interface ICryptoList {
    marketData: ICryptoMarketData[];
    loading: boolean;
  }