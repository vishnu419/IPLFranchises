
import { Component, OnInit } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { OrderService } from '../core/services/order.service';
import { OrderResponse } from '../core/models/order.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-order-history',
  standalone: true,
  imports: [NgIf, NgFor, DatePipe],
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.css']
})
export class OrderHistoryComponent implements OnInit {
  orders: OrderResponse[] = [];
  loading = false;
  error: string | null = null;
  page = 1;
  pageSize = 10;
  totalCount = 0;

  constructor(private orderService: OrderService) {}

  ngOnInit() {
    this.fetchOrders();
  }

  fetchOrders() {
    this.loading = true;
    this.error = null;
    this.orderService.getPagedOrders(this.page, this.pageSize).subscribe({
      next: res => {
        this.orders = res.items;
        this.totalCount = res.totalCount;
        this.loading = false;
      },
      error: err => {
        this.error = 'Failed to load orders.';
        this.loading = false;
      }
    });
  }

  onPageChange(page: number) {
    if (page < 1 || page > this.totalPages) return;
    this.page = page;
    this.fetchOrders();
  }

  get totalPages() {
    return Math.max(1, Math.ceil(this.totalCount / this.pageSize));
  }
}
