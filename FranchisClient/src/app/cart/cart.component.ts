
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../core/services/cart.service';
import { OrderService } from '../core/services/order.service';
import { AuthService } from '../core/services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { OrderRequest } from '../core/models/order.model';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  state$ = this.cart.state$;

  placing = false;
  placed = false;
  error: string | null = null;

  constructor(
    public cart: CartService,
    private orderService: OrderService,
    public auth: AuthService,
    private router: Router
  ) {}


  updateQty(id: number, qty: number) {
    this.cart.updateQuantity(id, qty);
  }


  remove(id: number) {
    this.cart.remove(id);
  }


  clear() {
    this.cart.clear();
  }

  placeOrder() {
    if (!this.auth.isLoggedIn) {
      this.router.navigate(['/login'], { queryParams: { returnUrl: '/cart' } });
      return;
    }
    this.placing = true;
    this.error = null;
    this.placed = false;
    const items = this.cart.currentState.items.map(i => ({
      productId: i.productId.toString(),
      quantity: i.quantity,
      price: i.price
    }));
    const req: OrderRequest = { items };
    this.orderService.placeOrder(req).subscribe({
      next: () => {
        this.placing = false;
        this.placed = true;
        this.cart.clear();
        setTimeout(() => {
          this.placed = false;
        }, 2500);
      },
      error: err => {
        this.placing = false;
        this.error = 'Failed to place order.';
      }
    });
  }
}
