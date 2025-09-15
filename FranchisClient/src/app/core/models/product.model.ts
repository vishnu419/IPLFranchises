export interface Product {
  id: number;
  name?: string | null;
  categoryName?: string | null;
  price: number;
  franchiseName?: string | null;
  imageUrl?: string | null;
  description?: string | null;
  images?: string[] | null;
}

export interface ProductPagedResult {
  items: Product[] | null;
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
