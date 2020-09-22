import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'my-auth-token'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ChartsService {
  private url = "http://localhost:5001/api/charts/hot-100";

  constructor(private http: HttpClient) { }

  getCharts() {
    return this.http.get(this.url, httpOptions);
  }
}