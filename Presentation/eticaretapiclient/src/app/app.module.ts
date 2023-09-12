import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';
import { AppRoutingModule } from './app-routing,module';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';


@NgModule({
  declarations: [
    AppComponent
     
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AdminModule, UiModule,
    ToastrModule.forRoot(),
    NgxSpinnerModule,
    HttpClientModule, 
    MatButtonModule
  ],
  providers: [
    {
      provide: "baseUrl", useValue: "https://localhost:7116/api", multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
