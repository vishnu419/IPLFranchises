import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartItem, CartState } from '../models/cart.model';

const STORAGE_KEY = 'ipl_cart_v1';

@Injectable({ providedIn: 'root' })
export class CartService {
  private state: CartState = { items: [] };
  get currentState(): CartState {
    return this.state;
  }
  private subject = new BehaviorSubject<CartState>(this.state);
  state$ = this.subject.asObservable();

  constructor() {
    this.load();
  }

  private emit() {
    this.subject.next(this.state);
    this.save();
  }

  private save() {
    localStorage.setItem(STORAGE_KEY, JSON.stringify(this.state));
  }

  private load() {
    const raw = localStorage.getItem(STORAGE_KEY);
    if (raw) {
      try { this.state = JSON.parse(raw); } catch {}
    }
    this.subject.next(this.state);
  }

  get count(): number { return this.state.items.reduce((s,i)=> s + i.quantity, 0); }
  get total(): number { return this.state.items.reduce((s,i)=> s + i.quantity * i.price, 0); }

  add(item: Omit<CartItem, 'quantity'>, qty = 1) {
    const existing = this.state.items.find(i => i.productId === item.productId);
    if (existing) existing.quantity += qty; else this.state.items.push({ ...item, quantity: qty });
    this.emit();
  }

  updateQuantity(productId: number, qty: number) {
    const it = this.state.items.find(i => i.productId === productId);
    if (!it) return;
    it.quantity = Math.max(1, qty);
    this.emit();
  }

  remove(productId: number) {
    this.state.items = this.state.items.filter(i => i.productId !== productId);
    this.emit();
  }

  clear() {
    this.state.items = [];
    this.emit();
  }
}
