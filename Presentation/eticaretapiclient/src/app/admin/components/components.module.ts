import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsModule } from './products/products.module';
import { CustomersModule } from './customers/customers.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { OrdersModule } from './orders/orders.module';
import { MatPaginatorModule } from '@angular/material/paginator';  

@NgModule({
  declarations: [  ],
  imports: [
    CommonModule,
    ProductsModule,
    OrdersModule,
    CustomersModule,
    DashboardModule,
    MatPaginatorModule,   
  ]
})
export class ComponentsModule { }
