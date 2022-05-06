export interface CurrencyConverterResponse {
    date: string;
    result: number;
    query: Query;
    info: Info;
    success: boolean;
    responseDescription: string;
    currencyDescription: string;
}

export interface Query {
    amount: number;
    from: string;
    to: string;
}

export interface Info {
    rate: number;
    timeStamp: number;
}