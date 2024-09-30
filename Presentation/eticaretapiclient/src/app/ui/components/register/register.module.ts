import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RegisterComponent } from './register.component';
import { CustomersComponent } from '../../../admin/components/customers/customers.component';



@NgModule({
  declarations: [
  ],
  imports:
    [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: RegisterComponent }
    ])
   ]
 })
export class RegisterModule { }
