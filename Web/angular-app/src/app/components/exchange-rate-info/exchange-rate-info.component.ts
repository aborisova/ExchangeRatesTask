import { Component, OnInit, Input } from '@angular/core';
import { ExchangeRate } from 'src/app/ExchangeRate';

@Component({
  selector: 'app-exchange-rate-info',
  templateUrl: './exchange-rate-info.component.html',
  styleUrls: ['./exchange-rate-info.component.css']
})
export class ExchangeRateInfoComponent implements OnInit {
  @Input() rate: ExchangeRate;

  constructor() {}

  ngOnInit(): void {}

}
