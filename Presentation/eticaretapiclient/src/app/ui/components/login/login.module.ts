import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LoginComponent} from "@app/ui/components/login/login.component";
import {ReactiveFormsModule} from "@angular/forms";
import {RouterModule} from "@angular/router";
import {ToastrModule} from "ngx-toastr";


@NgModule({
  declarations: [
      LoginComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
      ToastrModule,
    RouterModule.forChild([
      { path: '', component: LoginComponent }
    ])
  ]
})
export class LoginModule { }
