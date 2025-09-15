import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface FranchiseResponse {
  id: string;
  name: string;
}

@Injectable({ providedIn: 'root' })
export class FranchiseService {
  private api = environment.apiBaseUrl + '/api/franchise';

  constructor(private http: HttpClient) {}

  getAll(): Observable<FranchiseResponse[]> {
    return this.http.get<FranchiseResponse[]>(this.api);
  }
}
