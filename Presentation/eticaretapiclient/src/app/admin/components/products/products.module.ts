import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products.component';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator'; 
import { MatIconModule } from '@angular/material/icon'; 
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';  
import { DeleteButtonComponent } from '../../../WrapperComponents/delete-button/delete-button.component';
  
@NgModule({
  declarations: [
    ProductsComponent,
    CreateComponent,
    ListComponent, DeleteButtonComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: ProductsComponent },

    ]),
    MatSidenavModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule, MatPaginatorModule,
    FontAwesomeModule,
    MatIconModule
  ]
})
export class ProductsModule { }
