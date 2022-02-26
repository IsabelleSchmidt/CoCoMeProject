import { Injectable } from '@angular/core';
import { PaymentMethod } from './sale.model';
import { HttpClient } from "@angular/common/http";
import { HttpBaseService } from './http-base.service';
import { BehaviorSubject, catchError, Observable, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SaleService {

  constructor(private readonly httpBase: HttpBaseService) {
}
  readonly endpointSale = "Sale";
  public isEx = new BehaviorSubject(false);

  public isExpress = this.isEx.asObservable();

  public awaitC = new BehaviorSubject(false);
  public awaitCard = this.awaitC.asObservable();
  public openC = new BehaviorSubject(false);
  public openCash = this.openC.asObservable();

 

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
  
  
  addSale(pm: boolean) {
    console.log("pay " + pm);
    if (pm) {
      return this.httpBase.get(this.endpointSale+"/add/1").subscribe();
    }
    return this.httpBase.get(this.endpointSale+"/add/0").subscribe();
    
    
  }
  getExpress():void {

    this.httpBase.get<boolean>(this.endpointSale + "/express").pipe(tap(_ => this.log('fetched items')),
      catchError(this.handleError<boolean>('getItems', false))
    ).subscribe(expr => this.isEx.next(expr));

  }
  deleteExpiredSales(): void {
    this.httpBase.get(this.endpointSale + "/removeexpired").subscribe(i => this.getExpress());
  }

  stopExpress(): void {
    //clear sales
    
    this.httpBase.get(this.endpointSale + "/clear").subscribe(i => this.getExpress());
  }
  closeCashbox(result: boolean) {
    if(result) this.addSale(true);
    this.openC.next(!result);
  }
  acceptCard(result: boolean) {
    if (result) this.addSale(false);
    this.awaitC.next(!result);
  }
 }
