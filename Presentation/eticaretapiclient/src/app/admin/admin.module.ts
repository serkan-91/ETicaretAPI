import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from './layout/layout.module';
import { LayoutComponent } from './layout/layout.component';
import { ComponentsModule } from './components/components.module';
import { DeleteDirective } from '../directives/admin/delete.directive';



@NgModule({
  declarations: [],
  imports: [
    CommonModule, LayoutModule
  ],
  exports: [
    LayoutComponent,
    ComponentsModule
  ]
})
export class AdminModule { }
