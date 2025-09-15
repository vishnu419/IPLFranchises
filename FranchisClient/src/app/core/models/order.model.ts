export interface OrderRequest {
  items: OrderItemRequest[];
}

export interface OrderItemRequest {
  productId: string; // Guid
  quantity: number;
  price: number;
}

export interface OrderResponse {
  id: string; // Guid
  orderDate: string;
  items: OrderItemResponse[];
}

export interface OrderItemResponse {
  productId: string; // Guid
  productName: string;
  quantity: number;
  price: number;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
