import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ProductService } from '../core/services/product.service';
import { CartService } from '../core/services/cart.service';
import { Product } from '../core/models/product.model';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {
  product: Product | null = null;
  loading = false;
  error: string | null = null;
  selectedImage: string | null = null;
  cartProductIds = new Set<string | number>();
  private cartSub?: any;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cart: CartService
  ) {}

  ngOnInit() {
    this.cartSub = this.cart.state$.subscribe(state => {
      this.cartProductIds = new Set(state.items.map(i => i.productId));
    });
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loading = true;
      this.productService.getProductById(id).subscribe({
        next: (p: Product) => {
          this.product = p;
          if (p.images && p.images.length > 0) {
            this.selectedImage = p.images[0];
          } else {
            this.selectedImage = p.imageUrl || 'assets/ipl1.png';
          }
          this.loading = false;
        },
        error: (err: any) => {
          this.error = 'Product not found.';
          this.loading = false;
        }
      });
    }
  }

  ngOnDestroy() {
    if (this.cartSub) this.cartSub.unsubscribe();
  }

  selectImage(img: string) {
    this.selectedImage = img;
  }

  addToCart(product: Product) {
    if (this.isInCart(product.id)) return;
    this.cart.add({
      productId: product.id,
      name: product.name ?? 'Product',
      price: product.price,
      imageUrl: this.selectedImage || product.imageUrl || 'assets/ipl1.png'
    }, 1);
  }

  isInCart(productId: string | number | undefined | null): boolean {
    if (productId == null) return false;
    return this.cartProductIds.has(productId);
  }
}

