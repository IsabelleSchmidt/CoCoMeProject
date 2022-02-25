import { Injectable } from '@angular/core';
import { catchError, Observable, of, tap } from 'rxjs';
import { CheckoutItem } from './checkout-item.model';
import { HttpBaseService } from './http-base.service';
import { Sale } from './sale.model';

@Injectable({
  providedIn: 'root'
})
export class CashRegisterService {

  constructor(private httpBase: HttpBaseService) { }
  readonly endpoint = "checkoutItem";
  readonly endpointSale = "sale";
  readonly endpointItem = "item";
  
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
  getAllItems(): Observable<CheckoutItem[]> {
    return this.httpBase.get<CheckoutItem[]>(this.endpoint).pipe(tap(_ => this.log('fetched items')),
      catchError(this.handleError<CheckoutItem[]>('getItems', []))
    );
  }
  getSum(): Observable<number> {
    return this.httpBase.get<number>(this.endpoint+"/sum").pipe(tap(_ => this.log('fetched items')),
      catchError(this.handleError<number>('getItems', 0))
    );
  }
  addPayment(sale: Sale): void {

    this.httpBase.post(this.endpointSale, sale).subscribe();

  }

  itemPlusOne(): void {

    this.httpBase.get(this.endpoint + "/plusone").subscribe();

  }
  deleteLast(): void {

    this.httpBase.get(this.endpoint + "/deletelast").subscribe();

  }
  addById(id: number): void {
    this.httpBase.get(this.endpointItem + "/" + id).subscribe();
  }
  clear(): void {
    this.httpBase.delete(this.endpoint).subscribe();
  }
  getExpress(): Observable<boolean> {
    return this.httpBase.get<boolean>(this.endpointSale + "/express").pipe(tap(_ => this.log('fetched items')),
      catchError(this.handleError<boolean>('getItems', false))
    );
  }
  deleteExpiredSales(): void {
    this.httpBase.get(this.endpointSale + "/removeexpired").subscribe();
  }
}
