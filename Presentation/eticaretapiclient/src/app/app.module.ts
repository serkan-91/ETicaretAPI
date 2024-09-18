import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';
import { AppRoutingModule } from './app-routing.module';

import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import $ from 'jquery';
import { CommonModule } from '@angular/common';  // CommonModule'ü ekliyoruz
import { MatSidenavModule } from '@angular/material/sidenav';
import { CsrfInterceptorService } from './services/interceptions/csrf-interceptor.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  bootstrap: [AppComponent],
  exports: [
    MatSidenavModule
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AdminModule, UiModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule
  ],
  providers: [
    {
      provide: "baseUrl", useValue: "https://localhost:7116/api",
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CsrfInterceptorService,
      multi: true

    },
    provideHttpClient(withInterceptorsFromDi()),
    provideAnimationsAsync()
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
$(function () {
});
