export interface IProviderSelector {
    selectedProvider: string;
    onProviderChange: (provider: string) => void;
}
