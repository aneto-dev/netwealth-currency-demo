
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { MessageService } from './message.service';
import { Country } from 'src/app/models/country';
import { CurrencyRequestModel } from 'src/app/models/currency-request';
import { GlobalComponent } from 'src/app/globalComponent';

@Injectable({
  providedIn: 'root'
})
export class CurrenciesService {

   apiUrl = GlobalComponent.apiUrl
   key = GlobalComponent.key;
  
  constructor(private http: HttpClient, private messageService: MessageService) { 

  }

  public products: Country[] = [];

  getCountryCurrencies(): Observable<Country[]>{

      let headers: HttpHeaders = new HttpHeaders();
      headers = headers.append('apikey', this.key);

      return this.http.get<Country[]>(this.apiUrl + "/currency",{headers:  headers }  )
        .pipe(
        tap(_ => this.log('fetched countries')),
        catchError(this.handleError<Country[]>('getCountries', []))
      );
  }

  convertCurrency(postData: CurrencyRequestModel): Observable<Country[]>{

   

    let headers: HttpHeaders = new HttpHeaders();
     headers = headers.append('apikey', this.key);

      return this.http.post<Country[]>( this.apiUrl + "/currency", postData, 
      {headers:  headers }  )
        .pipe(
        tap(_ => this.log('fetched countries')),
        catchError(this.handleError<Country[]>('getCountries', []))
      );
  }

  /** POST: add a new hero to the server */
// addHero(hero: Hero): Observable<Hero> {
//   return this.http.post<Hero>(this.heroesUrl, hero, this.httpOptions).pipe(
//     tap((newHero: Hero) => this.log(`added hero w/ id=${newHero.id}`)),
//     catchError(this.handleError<Hero>('addHero'))
//   );
// }

    /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
     private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
  
        // TODO: send the error to remote logging infrastructure
        console.error(error); // log to console instead
  
        // TODO: better job of transforming error for user consumption
        this.log(`${operation} failed: ${error.message}`);
  
        // Let the app keep running by returning an empty result.
        return of(result as T);
      };
    }
  
    /** Log a HeroService message with the MessageService */
    private log(message: string) {
      this.messageService.add(`CountriesService: ${message}`);
    }
}
