import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../core/services/auth.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule]
})
export class ForgotPasswordComponent {
  forgotForm: FormGroup;
  loading = false;
  message: string | null = null;

  constructor(private fb: FormBuilder, private auth: AuthService) {
    this.forgotForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit() {
    if (this.forgotForm.invalid) return;
    this.loading = true;
    this.message = null;
    this.auth.forgotPassword(this.forgotForm.value.email).subscribe({
      next: () => {
        this.message = 'If the email exists, a reset link will be sent.';
      },
      error: () => {
        this.message = 'Request failed.';
      },
      complete: () => this.loading = false
    });
  }
}
