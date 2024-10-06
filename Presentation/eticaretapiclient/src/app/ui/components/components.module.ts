import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketsModule } from './baskets/baskets.module';
import { HomeModule } from './home/home.module';
import { ProductsModule } from './products/products.module';
import { RegisterModule } from './register/register.module';
import {LoginModule} from "@app/ui/components/login/login.module";

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    ProductsModule,
    BasketsModule,
    HomeModule,
    RegisterModule,
    LoginModule
  ]
})
export class ComponentsModule { }
