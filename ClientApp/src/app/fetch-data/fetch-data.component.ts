import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as moment from 'moment';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: FeedItem[] = [];
  scrollDistance: Number = 1;
  throttle: Number = 50;
  page = 0;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<FeedItem[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

  formatDate(data: string){
    moment.locale('es');
    var som = moment(data).format("MMMM DD YYYY, h:mm:ss a");
   
    return som;
 }

 onScroll() {
  this.page +=  1;
  fetch('api/SampleData/GetNextNews?page='+ this.page)
      .then(response => response.json() as Promise<FeedItem[]>)
      .then(data => {
          if(data == null || data == undefined) return;
          this.forecasts =  this.forecasts.concat(data);
          console.log(this.forecasts);
          console.log("Scrolled!!");
      });
}

}

interface FeedItem {
  url: string;
  lastUpdated: string;
  published: string;
  title: string;
  description: string;
}

