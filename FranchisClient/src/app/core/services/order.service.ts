import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { OrderRequest, OrderResponse, PagedResult } from '../models/order.model';

@Injectable({ providedIn: 'root' })
export class OrderService {
  private api = environment.apiBaseUrl + '/api/order';

  constructor(private http: HttpClient) {}

  placeOrder(request: OrderRequest): Observable<any> {
    return this.http.post(this.api, request);
  }

  getOrders(): Observable<OrderResponse[]> {
    return this.http.get<OrderResponse[]>(this.api);
  }

  getPagedOrders(pageNumber = 1, pageSize = 10): Observable<PagedResult<OrderResponse>> {
    return this.http.get<PagedResult<OrderResponse>>(`${this.api}/paged`, {
      params: { pageNumber, pageSize }
    });
  }
}
