import { Product } from './product.model';

export interface CartItem {
  productId: number | string;
  name: string;
  price: number;
  imageUrl?: string | null;
  quantity: number;
}

export interface CartState {
  items: CartItem[];
}
