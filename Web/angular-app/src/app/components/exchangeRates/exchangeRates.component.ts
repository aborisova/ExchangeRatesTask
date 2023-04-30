import { Component, OnInit } from '@angular/core';
import { ExchangeRate } from 'src/app/ExchangeRate';
import { RateParams } from 'src/app/RateParams';
import { ExchangeRateService } from 'src/app/services/exchangeRate.service';

@Component({
  selector: 'app-exchangeRates',
  templateUrl: './exchangeRates.component.html',
  styleUrls: ['./exchangeRates.component.css']
})
export class ExchangeRatesComponent  implements OnInit {
  
  constructor(private exchangeRateService: ExchangeRateService) {}
  exchangeRate: ExchangeRate ;
  ngOnInit(): void {    
  }
  getRate(params: RateParams) {
    this.exchangeRateService.getExchangeRate(params).subscribe((res) => this.exchangeRate = res);
  }
}
