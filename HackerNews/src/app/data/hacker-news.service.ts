import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Story } from './story';
@Injectable({
  providedIn: 'root'
})
export class HackerNewsService {

  constructor(private http: HttpClient) { }

  getStories() : Observable<Story[]> {
    return this.http.get<Story[]>('api/story')
    .pipe(catchError(this.handleError<Story[]>('getStories',[])))
  }

  //handles the errors
  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any) : Observable<T> => {
      console.error(error);
      return of(result as T);
    }
  }

}
