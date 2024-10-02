import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ProductService } from '@app/services/common/models/product.service';
import { Product } from "@app/contracts/list_product";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent  implements OnInit {
  @Output() productCreated = new EventEmitter<Product>();
  productForm!: FormGroup; 

  constructor(private fb: FormBuilder,
              private productService: ProductService) { }

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      stock: [0, [Validators.required, Validators.min(1)]],
      price: [0, [Validators.required, Validators.min(0)]]
    });
  } 
  onSubmit() {
    if (this.productForm.valid) {
      const product = this.productForm.value;
      this.productService.create(product).subscribe((response: Product) => {
        this.productCreated.emit(response); 
        this.productForm.reset();

      })
    }
    
      this.productForm.reset({
        name: "",
        price: 1,
        stock: 1
      });
    }
  }  

