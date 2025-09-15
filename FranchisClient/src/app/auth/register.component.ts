import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../core/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class RegisterComponent {
  registerForm: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit() {
    if (this.registerForm.invalid) return;
    this.loading = true;
    this.error = null;
    this.auth.register(this.registerForm.value).subscribe({
      next: user => {
        // Optionally store user info/token
        this.router.navigate(['/login']);
      },
      error: err => {
        this.error = err.error || 'Registration failed';
        this.loading = false;
      },
      complete: () => this.loading = false
    });
  }
}
