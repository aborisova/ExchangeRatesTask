import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ExchangeRatesComponent } from './components/exchangeRates/exchangeRates.component';
import { SelectFormComponent } from './components/select-form/select-form.component';
import { FormsModule } from '@angular/forms';
import { ExchangeRateInfoComponent } from './components/exchange-rate-info/exchange-rate-info.component';


@NgModule({
  declarations: [
    AppComponent,
    ExchangeRatesComponent,
    SelectFormComponent,
    ExchangeRateInfoComponent
  ],
  imports: [
    FormsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
