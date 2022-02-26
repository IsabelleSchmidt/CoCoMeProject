import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { CheckoutItem } from './checkout-item.model';
import { Item } from './item.model';

@Injectable({
  providedIn: 'root'
})
export class StoreServerService {

  constructor(private http: HttpClient) { }

  private readonly url = "https://localhost:7071/api";
  
  private log(message: string) {
    console.log(`ItemService: ${message}`);
  }
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); 
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
  getAllItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.url + "/fetchitems").pipe(tap(_ => this.log('fetched items')),
      catchError(this.handleError<Item[]>('getItems', []))
    );

    
  }
  removeItemsStore(items: CheckoutItem[]): void {
    items.forEach(i => this.removeSingleFromStore(i));
  }
  removeSingleFromStore(item: CheckoutItem): void {

    this.http.get(this.url + "/removeitems/" + item.itemId + "/" + item.amount).subscribe();

  }
  
  
}
