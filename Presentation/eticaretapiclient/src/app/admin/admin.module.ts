import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from './layout/layout.module';  // LayoutModule'ü import ediyoruz

@NgModule({
  declarations: [
    // Bu kısım boş kalabilir, LayoutComponent burada tanımlanmayacak
  ],
  imports: [
    CommonModule,
    LayoutModule  // LayoutComponent kullanmak için LayoutModule burada import edildi
  ] 
})
export class AdminModule { }
