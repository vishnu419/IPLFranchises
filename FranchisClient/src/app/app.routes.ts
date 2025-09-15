import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./layout/main-layout.component').then(m => m.MainLayoutComponent),
    children: [
  { path: '', loadComponent: () => import('./home/home.component').then(m => m.HomeComponent) },
  { path: 'products', loadComponent: () => import('./products/product-grid.component').then(m => m.ProductGridComponent) },
  { path: 'products/:id', loadComponent: () => import('./products/product-detail.component').then(m => m.ProductDetailComponent) },
  { path: 'cart', loadComponent: () => import('./cart/cart.component').then(m => m.CartComponent) },
  { path: 'login', loadComponent: () => import('./auth/login.component').then(m => m.LoginComponent) },
  { path: 'register', loadComponent: () => import('./auth/register.component').then(m => m.RegisterComponent) },
  { path: 'forgot-password', loadComponent: () => import('./auth/forgot-password.component').then(m => m.ForgotPasswordComponent) },
  { path: 'profile', loadComponent: () => import('./user/profile.component').then(m => m.ProfileComponent) },
  { path: 'orders', loadComponent: () => import('./orders/order-history.component').then(m => m.OrderHistoryComponent) },
  { path: '**', redirectTo: '' }
    ]
  }
];
