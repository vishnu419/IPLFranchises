import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  username: string;
  email: string;
  userId: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private api = environment.apiBaseUrl + '/api/user';
  private userSubject = new BehaviorSubject<AuthResponse | null>(this.getUserFromStorage());
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) {}

  login(data: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.api}/login`, data).pipe(
      tap(res => {
        localStorage.setItem('auth', JSON.stringify(res));
        this.userSubject.next(res);
      })
    );
  }

  register(data: RegisterRequest): Observable<any> {
    return this.http.post<any>(`${this.api}/register`, data);
  }

  forgotPassword(email: string): Observable<any> {
    return this.http.post(`${this.api}/forgot-password`, { email });
  }

  logout() {
    localStorage.removeItem('auth');
    this.userSubject.next(null);
  }

  getUserFromStorage(): AuthResponse | null {
    const data = localStorage.getItem('auth');
    return data ? JSON.parse(data) : null;
  }

  get isLoggedIn(): boolean {
    return !!this.userSubject.value;
  }
}
