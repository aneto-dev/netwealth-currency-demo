export class CurrencyRequestModel {

    constructor(
      public amount : number,
      public fromCurrencyReference: string,
      public toCurrencyReference : string

    ) {  }
  
  }