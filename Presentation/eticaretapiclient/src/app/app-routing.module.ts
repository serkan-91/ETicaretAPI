import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './admin/components/dashboard/dashboard.component';
import { LayoutComponent } from './admin/layout/layout.component';
import { ProductsComponent } from './admin/components/products/products.component';
import { OrdersComponent } from './admin/components/orders/orders.component';
import { HomeComponent } from './ui/components/home/home.component';

const routes: Routes = [
  {
    path: "admin", component: LayoutComponent,
    children: [
      { path: "", component: DashboardComponent },
      {
        path: "customers",
        loadChildren: () =>
          import("./admin/components/customers/customers.module").
            then(module => module.CustomersModule)
      },
      {
        path: "products", component: ProductsComponent,
        loadChildren: () =>
          import("./admin/components/products/products.module").
            then(module => module.ProductsModule)
      },
      {
        path: "orders", component: OrdersComponent,
        loadChildren: () =>
          import("./admin/components/orders/orders.module").
            then(module => module.OrdersModule)
      },
    ]
  },
  { path: "", component: HomeComponent },
  {
    path: "basket",
    loadChildren: () => import("./ui/components/baskets/baskets.module").
      then(module => module.BasketsModule)
  },
  {
    path: "products",
    loadChildren: () => import("./ui/components/products/products.module").
      then(module => module.ProductsModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }