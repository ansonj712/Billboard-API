import { Component, OnInit } from '@angular/core';

import { ChartsService } from '../charts.service';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnInit {

  constructor(private chartsService: ChartsService) { }

  ngOnInit(): void {
    this.getCharts();
  }

  getCharts(): void {
    this.chartsService.getCharts()
      .subscribe((data) => {
        console.log(data);
      });
  }
}
