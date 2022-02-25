import { Injectable } from '@angular/core';
import { Item } from './item.model';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { HttpBaseService } from './http-base.service';
import { CheckoutItem } from './checkout-item.model';
import { catchError, map, Observable, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private httpBase: HttpBaseService, private httpClient: HttpClient){}
  readonly endpoint = "item";

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
  getAllItems(): Observable<Item[]>
  {
    return this.httpBase.get<Item[]>(this.endpoint).pipe(tap(_ => this.log('fetched items')),
      catchError(this.handleError<Item[]>('getItems', []))
    );
  }
  getItem(id: number)
  {
    const url = `https://localhost:44320/${this.endpoint}/${id}`;
    return this.httpBase.get<Item[]>(this.endpoint+"/"+id)
      .pipe(
        map(heroes => heroes[0]), // returns a {0|1} element array
        tap(h => {
          const outcome = h ? 'fetched' : 'did not find';
          this.log(`${outcome} item id=${id}`);
        }),
        catchError(this.handleError<Item>(`getItem id=${id}`))
      );
  }
  addItem(item: Item): void {
   

    this.httpBase.post<CheckoutItem>(this.endpoint, item).subscribe();
    /*.pipe(
      tap((newHero: CheckoutItem) => this.log(`added item w/ id=${newHero.itemId}`)),
        catchError(this.handleError<CheckoutItem>('addItem'))
      );*/
    
    

    
  }
  
  
 }
