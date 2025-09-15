import { Component } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CartService } from '../core/services/cart.service';
import { AuthService, AuthResponse } from '../core/services/auth.service';
import { map } from 'rxjs';
import { FooterComponent } from './footer.component';

@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [RouterModule, CommonModule, FooterComponent],
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.css']
})
export class MainLayoutComponent {
  cartCount$ = this.cart.state$.pipe(map(s => s.items.reduce((sum, i) => sum + i.quantity, 0)));
  user: AuthResponse | null = null;
  showMenu = false;

  constructor(public cart: CartService, public auth: AuthService, private router: Router) {
    this.auth.user$.subscribe(u => this.user = u);
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  goToProfile() {
    this.router.navigate(['/profile']);
  }

  goToOrders() {
    this.router.navigate(['/orders']);
  }
}
