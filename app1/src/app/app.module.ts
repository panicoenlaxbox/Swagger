import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { API_BASE_URL } from './foo.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [{ provide: API_BASE_URL, useValue: 'https://localhost:44337' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
