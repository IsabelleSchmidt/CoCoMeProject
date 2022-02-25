import { Injectable } from '@angular/core';
import { Sale } from './sale.model';
import { HttpClient } from "@angular/common/http";
import { HttpBaseService } from './http-base.service';

@Injectable({
  providedIn: 'root'
})
export class SaleService {

  constructor(private readonly httpBase: HttpBaseService) {
}
  readonly endpoint = "Sale";

 
  getSale(id: number) {
    return this.httpBase.get<Sale>(`${this.endpoint}/${id}`).subscribe();
  }
  deleteSales() {
    return this.httpBase.delete(this.endpoint).subscribe();
  }
  addSale(sale: Sale) {
    return this.httpBase.post(this.endpoint, sale).subscribe();
  }

  
 }
