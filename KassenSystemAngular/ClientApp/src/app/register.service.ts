import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { HttpBaseService } from './shared/http-base.service';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private httpBase: HttpBaseService) { }
  readonly endpoint = "home";
  
  private log(message: string) {
    console.log(`ItemService: ${message}`);
  }
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
  getId(): Observable<number> {
    return this.httpBase.get<number>(this.endpoint).pipe(tap(_ => this.log('fetched register')),
      catchError(this.handleError<number>('getRegister', 0))
    );
  }
}
