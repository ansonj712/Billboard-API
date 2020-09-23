import { Component, OnInit } from '@angular/core';

import { ChartsService } from '../charts.service';

import { ChartItem } from '../chartItem';
import { map, subscribeOn, toArray } from 'rxjs/operators';
import { DefaultUrlSerializer } from '@angular/router';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnInit {
  d = {}
  genres = [];
  public isMenuCollapsed = true;

  constructor(private chartsService: ChartsService) { }

  ngOnInit(): void {
    this.genres = [
      { 
        'name': 'Pop',
        'path': 'pop-songs' 
      }, 
      {
        'name': 'Dance',
        'path': 'dance-electronic-songs'
      }, 
      {
        'name': 'Country',
        'path': 'country-songs'
      },
      {
        'name': 'R&B/Hip Hop',
        'path': 'r-b-hip-hop-songs'
      }, 
      {
        'name': 'Rock',
        'path': 'rock-songs'
      },
      { 
        'name': 'Hot 100',
        'path': 'hot-100'
      },
      {
        'name': 'Billboard 200',
        'path': 'billboard-200'
      },
      {
        'name': 'Latin',
        'path': 'latin-songs'
      },
      {
        'name': 'Jazz',
        'path': 'jazz-songs'
      },
      {
        'name': 'Christian',
        'path': 'christian-songs'
      }
    ];
    
    this.genres.forEach(genre => {
      this.getCharts(genre);
    });
  }

  getCharts(genre): void {
    this.chartsService.getCharts(genre.path)
      .subscribe(chart => {
        this.d[genre.name] = chart
      });
  }
}
