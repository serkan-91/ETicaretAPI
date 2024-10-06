import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import {User} from "@app/entities/user";
import {UserService} from "@app/services/common/models/user-service.service";
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "@app/services/ui/custom-toastr.service";


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm!: FormGroup;

  constructor(
      private fb: FormBuilder,
      private userService: UserService,
      private toastrService: CustomToastrService
  ) {
    this.registerForm = this.fb.group({
      fullName: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50)
      ]],
      userName: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50)
      ]],
      email: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50),
        Validators.email
      ]],
          password: ['', [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(50),
            Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$')
          ]],
      confirmPassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });
  }

  passwordMatchValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');
    if (password && confirmPassword) {
      confirmPassword.setErrors(password.value !== confirmPassword.value ? { mustMatch: true } : null);
    }
    return null;
  }

  onSubmit(data: User) {
    if (this.registerForm.invalid) return;
    this.userService.create(data).subscribe(result => {
      this.toastrService.message(result.Message, 'Kullanıcı Oluşturuldu', {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      } );
      this.registerForm.reset();
    });
  }
}


