import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Client, LoginUserCommand } from '../../../api/api-reference';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  });

  error = '';

  constructor(
    private fb: FormBuilder,
    private client: Client,
    private router: Router
  ) { }

  onSubmit() {
    if (this.loginForm.invalid) return;

    const command = new LoginUserCommand({
      email: this.loginForm.value.email!,
      password: this.loginForm.value.password!
    });

    this.client.login(command).subscribe({
      next: (result) => {
        debugger
        localStorage.setItem('jwt_token', result.token!);
        this.router.navigate(['/home']);
      },
      error: () => {
        debugger
        this.error = 'Invalid email or password';
      }
    });
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }
}
