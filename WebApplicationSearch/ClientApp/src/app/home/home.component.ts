import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public searchengs: ISearchEngines[];
  
  searchString = new FormControl('');
  constructor(private readonly http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  updateSearch() {
    this.searchengs = [];
    const params = new HttpParams().append('search', this.searchString.value);
    this.http.get<ISearchEngines[]>(this.baseUrl + 'api/google', {  params } ).subscribe(result => {
      if (result != undefined && result != null) {
        for (let i = 0; i < result.length; i++) {
          this.searchengs.push(result[i]);
        }
      } 
    }, error => console.error(error));
    this.http.get<ISearchEngines[]>(this.baseUrl + 'api/bing', { params }).subscribe(result => {
      if (result != undefined && result != null) {
        for (let i = 0; i < result.length; i++) {
          this.searchengs.push(result[i]);
        }
      } 
    }, error => console.error(error));
  };

}




interface ISearchEngines {
  searchEngine: string;
  request: string;
  title: string;
  enteredDate: string;
}
