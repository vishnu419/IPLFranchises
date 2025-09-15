import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../core/services/product.service';
import { CartService } from '../core/services/cart.service';
import { Product } from '../core/models/product.model';
import { RouterModule } from '@angular/router';
import { FranchiseService, FranchiseResponse } from '../core/services/franchise.service';

@Component({
  selector: 'app-product-grid',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './product-grid.component.html',
  styleUrls: ['./product-grid.component.css']
})
export class ProductGridComponent implements OnInit, OnDestroy {
  products: Product[] = [];
  page = 1;
  pageSize = 12;
  totalProducts = 0;
  loading = false;
  error: string | null = null;

  // filters/controls
  search = '';
  pageSizeOptions = [8, 12, 24, 48];
  franchises: FranchiseResponse[] = [];
  selectedFranchise: string = '';

  cartProductIds = new Set<string>();
  private cartSub?: any;

  constructor(
    private productService: ProductService,
    private cart: CartService,
    private franchiseService: FranchiseService
  ) {}

  ngOnInit() {
    this.loadProducts();
    this.franchiseService.getAll().subscribe(franchises => {
      this.franchises = franchises;
    });
    this.cartSub = this.cart.state$.subscribe(state => {
      this.cartProductIds = new Set(state.items.map(i => i.productId.toString()));
    });
  }

  ngOnDestroy() {
    if (this.cartSub) this.cartSub.unsubscribe();
  }

  get totalPages(): number {
    return Math.max(1, Math.ceil(this.totalProducts / this.pageSize));
  }

  loadProducts() {
    this.loading = true;
    this.error = null;
    this.productService.getProducts(this.page, this.pageSize).subscribe({
      next: (res) => {
        this.products = (res.items ?? []).map(p => ({ ...p, imageUrl: p.imageUrl ?? 'assets/ipl1.png' }));
        this.totalProducts = res.totalCount ?? this.products.length;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load products';
        this.loading = false;
        console.error(err);
      }
    });
  }

  onSearch() {
    if (!this.search.trim() && !this.selectedFranchise) {
      this.page = 1;
      this.loadProducts();
      return;
    }
    this.loading = true;
    this.error = null;
    this.productService.searchProducts(this.search, this.selectedFranchise).subscribe({
      next: (items) => {
        this.products = (items ?? []).map(p => ({ ...p, imageUrl: p.imageUrl ?? 'assets/ipl1.png' }));
        this.totalProducts = this.products.length;
        this.page = 1;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Search failed';
        this.loading = false;
        console.error(err);
      }
    });
  }

  onClearFilter() {
    this.search = '';
    this.selectedFranchise = '';
    this.page = 1;
    this.loadProducts();
  }

  onPageChange(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.page = page;
    this.loadProducts();
  }

  onPageSizeChange() {
    this.page = 1;
    this.loadProducts();
  }

  addToCart(p: Product) {
    this.cart.add({
      productId: p.id,
      name: p.name ?? 'Product',
      price: p.price,
      imageUrl: p.imageUrl ?? 'assets/ipl1.png'
    }, 1);
  }

  isInCart(productId: string | number): boolean {
    return this.cartProductIds.has(productId.toString());
  }
}

