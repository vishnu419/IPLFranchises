import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class LoginComponent {
  loginForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      // Mark all fields as touched to show validation errors
      Object.values(this.loginForm.controls).forEach(control => control.markAsTouched());
      return;
    }
    this.loading = true;
    this.error = null;
    this.auth.login(this.loginForm.value).subscribe({
      next: res => {
        this.router.navigate(['/']);
      },
      error: err => {
        this.error = err.error?.message || err.error || 'Login failed';
        this.loading = false;
      },
      complete: () => this.loading = false
    });
  }

  goToRegister() {
    this.router.navigate(['/register']);
  }

  goToForgotPassword() {
    this.router.navigate(['/forgot-password']);
  }
}
