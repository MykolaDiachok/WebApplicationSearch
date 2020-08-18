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
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  updateSearch() {
    const params = new HttpParams().append('search', this.searchString.value);
    this.http.get<ISearchEngines[]>(this.baseUrl + 'api/google', {  params } ).subscribe(result => {
      this.searchengs = result;
    }, error => console.error(error));
  };
}




interface ISearchEngines {
  searchEngine: string;
  request: string;
  title: string;
  enteredDate: string;
}
