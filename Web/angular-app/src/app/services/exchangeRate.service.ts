import { Injectable,Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { ExchangeRate } from '../ExchangeRate';
import { RateParams } from '../RateParams';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  }),
};

@Injectable({
  providedIn: 'root',
})
export class ExchangeRateService {
  private exchangeRateUrl = 'api/ExchangeRate';
  private currenciesUrl = 'api/Currencies';
  private baseUrlLocal:string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrlLocal = baseUrl;    
  }

  getExchangeRate(params: RateParams): Observable<ExchangeRate> {
    const url = `${this.baseUrlLocal}${this.exchangeRateUrl}?date=${params.date.toISOString()}&currencyCode=${params.currencyCode}`;
    return this.http.get<ExchangeRate>(url, httpOptions);
  }

  getCurrencies(): Observable<string[]> {
    const url = `${this.baseUrlLocal}${this.currenciesUrl}`;    
    return this.http.get<string[]>(url, httpOptions);
  }
}
