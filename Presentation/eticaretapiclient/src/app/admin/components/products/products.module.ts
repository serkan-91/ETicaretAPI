import { CommonModule, NgFor } from '@angular/common';
import { RouterModule } from '@angular/router';

import { ProductsComponent } from './products.component';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { DialogsModule } from '../../../dialogs/dialogs.module';
import { MatTableModule } from '@angular/material/table';
import { FileUploadComponent } from '../../../services/common/file-upload/file-upload.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { DeleteButtonComponent } from '../../../WrapperComponents/delete-button/delete-button.component';
import { DeleteDirective } from '../../../directives/admin/delete.directive';
import { NgModule } from '@angular/core';
import { NgxFileDropModule } from 'ngx-file-drop';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ProductsComponent,
    CreateComponent,
    ListComponent,
    DeleteDirective,
    DeleteButtonComponent
  ],
  imports: [
    RouterModule.forChild([
      { path: "", component: ProductsComponent },
    ]),
    CommonModule,
    MatButtonModule,
    MatSidenavModule,
    MatFormFieldModule,
    MatInputModule,
    MatPaginatorModule,
    MatIconModule,
    MatTableModule,
    DialogsModule,
    FontAwesomeModule,
    FormsModule
  ]
})
export class ProductsModule {
}
