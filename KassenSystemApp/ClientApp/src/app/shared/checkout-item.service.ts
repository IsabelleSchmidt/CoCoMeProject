import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, of, tap } from 'rxjs';
import { CheckoutItem } from './checkout-item.model';
import { HttpBaseService } from './http-base.service';
import { Item } from './item.model';

@Injectable({
  providedIn: 'root'
})
export class CheckoutItemService {

  constructor(private httpBase: HttpBaseService) { }
  readonly endpoint = "checkoutItem";

  public checkoutItems: CheckoutItem[] = [];
  public cItems = new BehaviorSubject([new CheckoutItem()]);
  public checkoutItemsTest = this.cItems.asObservable();
  public sumTotal: number = 0;
  public test = new BehaviorSubject(0);
  public sum= this.test.asObservable();
  
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
  getAllItems(): void {
    this.httpBase.get<CheckoutItem[]>(this.endpoint).pipe(tap(_ => this.log('fetched checkoutitems')),
      catchError(this.handleError<CheckoutItem[]>('getItems', []))
    ).subscribe(list => this.cItems.next(list));
  }
 
  getSum(): void {
    this.httpBase.get<number>(this.endpoint + "/sum").pipe(tap(_ => this.log('fetched sum')),
      catchError(this.handleError<number>('getSum', 0))
    ).subscribe(sum => this.test.next(sum));
  }
  itemPlusOne(): void {

    this.httpBase.get(this.endpoint + "/plusone").subscribe(i => this.update());

  }
  deleteLast(): void {

    this.httpBase.get(this.endpoint + "/deletelast").subscribe(i => this.update());

  }
  addById(id: number): void {
    this.httpBase.post(this.endpoint + "/" + id, id).subscribe(i => this.update());
  }
  addItem(item: Item): void {
    this.addById(item.id);
    //this.httpBase.post(this.endpoint, item).subscribe();
    
  }
  clear(): void {
    this.httpBase.get(this.endpoint + "/clear").subscribe(i => this.update());
  }
  getList(): Observable<CheckoutItem[]> {
    return this.httpBase.get<CheckoutItem[]>(this.endpoint).pipe(tap(_ => this.log('fetched checkoutitems')),
      catchError(this.handleError<CheckoutItem[]>('getItems', []))
    );
  }
  update(): void {
    this.getAllItems();
    this.getSum();
  }
}
