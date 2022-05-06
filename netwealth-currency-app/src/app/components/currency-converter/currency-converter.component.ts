import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Country } from 'src/app/models/country';
import { CurrencyRequestModel } from 'src/app/models/currency-request';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';

@Component({
  selector: 'currency-converter',
  templateUrl: './currency-converter.component.html',
  styleUrls: ['./currency-converter.component.css']
})
export class CurrencyConverterComponent implements OnInit {

  countries: Country[] = [];

   filteredCountries: Country[] = [];

   model = new CurrencyRequestModel(0.00,'', '');
   result : any;
   submitted = false;
   isBusy = false;

  constructor( public currenciesService : CurrenciesService, http: HttpClient) { 

  }

  ngOnInit(): void {

    this.currenciesService.getCountryCurrencies()
    .subscribe(countries => this.countries = countries);

  }

  onSubmit() { this.submitted = true; 

    this.isBusy = true
              
       this.currenciesService.convertCurrency(this.model).subscribe(result => {this.result = result; this.isBusy = false });
   }

   onSubmitSwich() { this.submitted = true; 

    this.isBusy = true
    var currentState =  this.model;
    
    var updatedState = new CurrencyRequestModel(this.model.amount, this.model.fromCurrencyReference, this.model.toCurrencyReference )

    this.currenciesService.convertCurrency(updatedState).subscribe(result =>{ this.result = result;
                                                                                      this.model = updatedState;
                                                                                      this.isBusy = false;
                                                                             });
  }

}
