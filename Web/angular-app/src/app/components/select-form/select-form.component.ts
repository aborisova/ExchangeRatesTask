import { Component, OnInit, Output, EventEmitter} from '@angular/core';
import { ExchangeRateService } from 'src/app/services/exchangeRate.service';
import { RateParams } from 'src/app/RateParams';

@Component({
  selector: 'app-select-form',
  templateUrl: './select-form.component.html',
  styleUrls: ['./select-form.component.css']
})
export class SelectFormComponent implements OnInit {
  @Output() onGetRate: EventEmitter<RateParams> = new EventEmitter();
  currentDate = (new Date()).toJSON().slice(0, 10) ;
  date: string = '';
  currencyOptions: string[] = [];
  selectedOption: string='';
  

  constructor(private exchangeRateService :ExchangeRateService) {
   
  }

  ngOnInit(): void {
    this.exchangeRateService.getCurrencies().subscribe(res => this.currencyOptions = res);
  }

  onSubmit() {
    if(!this.selectedOption){
      return;
    }
    const params: RateParams = {
      date: new Date(this.date),
      currencyCode : this.selectedOption
    };
    this.onGetRate.emit(params);
  }
}
