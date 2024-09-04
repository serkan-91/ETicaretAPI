import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';
import { AppRoutingModule } from './app-routing,module';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';


@NgModule({ declarations: [
        AppComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        AdminModule, UiModule,
        ToastrModule.forRoot(),
        NgxSpinnerModule,
        MatButtonModule], providers: [
        {
            provide: "baseUrl", useValue: "https://localhost:7116/api", multi: true
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideAnimationsAsync()
    ] })
export class AppModule { }
