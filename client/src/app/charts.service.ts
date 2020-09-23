import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { ChartItem } from './chartItem';

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
  private url = "http://localhost:5001/api/charts/";

  constructor(private http: HttpClient) { }

  getCharts(genre: string): Observable<ChartItem[]> {
    return this.http.get<ChartItem[]>(this.url + genre, httpOptions);
  }
}