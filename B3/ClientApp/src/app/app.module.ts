import { BrowserModule } from '@angular/platform-browser';
import { DEFAULT_CURRENCY_CODE, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { registerLocaleData } from '@angular/common';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InvestimentModule } from './components/investiment/investiment.module';
import { CommonModule } from '@angular/common';

import { LOCALE_ID } from '@angular/core';
import localePtBr from '@angular/common/locales/pt';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
    ]),
    BrowserAnimationsModule,
    InvestimentModule
  ],
  providers: [{provide: LOCALE_ID, useValue: 'pt-BR'}, {
    provide: DEFAULT_CURRENCY_CODE,
    useValue: 'BRL'
  },],
  bootstrap: [AppComponent]
})
export class AppModule { /**
 *
 */
constructor() {
  registerLocaleData(localePtBr, 'pt-BR');
}}
