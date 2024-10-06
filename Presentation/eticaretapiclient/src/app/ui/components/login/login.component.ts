import { Component } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {UserService} from "@app/services/common/models/user-service.service";
import {login_User} from "@app/contracts/users/login_user";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm!: FormGroup;
  constructor(
      private fb: FormBuilder,
      private userService: UserService,
  ) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required, Validators.required]],
      password: ['', [
         Validators.required,
        Validators.minLength(6),
        Validators.maxLength(50),
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$')
      ] ]
    });
  }

  onSubmit(loginData: login_User) {
    if (this.loginForm.invalid) return;
    this.userService.login(loginData)
        .subscribe({
          next:  (response : login_User) => {
            console.log('Login successful'+ response);
          }
        });
  }
}
