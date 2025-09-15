import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product, ProductPagedResult } from '../models/product.model';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  getProducts(pageNumber: number, pageSize: number): Observable<ProductPagedResult> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber)
      .set('pageSize', pageSize);
    return this.http.get<ProductPagedResult>(`${this.baseUrl}/api/Product/paged`, { params });
  }

  searchProducts(name: string, franchiseId?: string): Observable<Product[]> {
    let params = new HttpParams().set('name', name);
    if (franchiseId) {
      params = params.set('franchiseId', franchiseId);
    }
    return this.http.get<Product[]>(`${this.baseUrl}/api/Product/search`, { params });
  }
  getProductById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/api/Product/${id}`);
  }
}
