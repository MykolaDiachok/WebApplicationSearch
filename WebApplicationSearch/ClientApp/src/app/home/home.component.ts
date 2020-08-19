import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { FormControl } from '@angular/forms';
import { Observable, forkJoin } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  searchEngs: ISearchEngines[];
  searchStatus: number;
  
  searchString = new FormControl('');
  constructor(private readonly http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  updateSearch() {
    this.searchEngs = [];
    this.searchStatus = 2;
    const params = new HttpParams().append('search', this.searchString.value);


    const google = this.http.get<ISearchEngines[]>(this.baseUrl + 'api/google', { params });
    const bing = this.http.get<ISearchEngines[]>(this.baseUrl + 'api/bing', { params });
    forkJoin([google, bing]).subscribe(result => {
      result.forEach((item: ISearchEngines[]) => {
          item.forEach((searchResult) => {
            this.searchEngs.push(searchResult);
          });
      });
      this.searchStatus --;
    },
      error => console.error(error),
      () => {
        this.searchStatus = 0;
      });

  };

}



interface ISearchEngines {
  searchEngine: string;
  request: string;
  title: string;
  enteredDate: Date;
}
