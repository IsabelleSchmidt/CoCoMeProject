import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { CheckoutItem } from '../../shared/checkout-item.model';
import { CheckoutItemService } from '../../shared/checkout-item.service';

@Component({
  selector: 'app-customer-display',
  templateUrl: './customer-display.component.html',
  styles: [
  ]
})
export class CustomerDisplayComponent implements OnInit {
  
  sumSubscription: Subscription;
  itemSubsription: Subscription;
  sumTotal = 0;
  checkoutItems: CheckoutItem[] = [];
  constructor(public service: CheckoutItemService) { }

  ngOnInit(): void {
    this.sumSubscription = this.service.sum.subscribe(message => this.sumTotal = message);
    this.itemSubsription = this.service.checkoutItemsTest.subscribe(list => this.checkoutItems = list);

    this.getItems();
      
    }
  getItems(): void {
    this.service.getAllItems();
    this.service.getSum();
  }
  
}
