import { ICryptoPrices } from "./ICryptoPrices";

export interface ICryptoPricesList{
    marketPrice: ICryptoPrices | undefined,
    loading: boolean
}