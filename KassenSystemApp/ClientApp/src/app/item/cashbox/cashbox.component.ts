import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SaleService } from '../../shared/sale.service';

@Component({
  selector: 'app-cashbox',
  templateUrl: './cashbox.component.html',
  styles: [
  ]
})
export class CashboxComponent implements OnInit {

  constructor(public saleService: SaleService) { }

  cashboxSubscription: Subscription;
  openCashbox: boolean = false;

  ngOnInit(): void {
    this.cashboxSubscription = this.saleService.openCash.subscribe(oc => this.openCashbox = oc);
  }
  closeCashbox(): void {
    if(this.openCashbox) this.saleService.closeCashbox(true);
  }

}
