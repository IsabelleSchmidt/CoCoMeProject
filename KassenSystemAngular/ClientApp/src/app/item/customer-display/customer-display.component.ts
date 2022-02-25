import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CheckoutItem } from '../../shared/checkout-item.model';
import { CheckoutItemService } from '../../shared/checkout-item.service';

@Component({
  selector: 'app-customer-display',
  templateUrl: './customer-display.component.html',
  styles: [
  ]
})
export class CustomerDisplayComponent implements OnInit {
  
  items: CheckoutItem[] = [];
  itemSum: number = 0;
  constructor(public service: CheckoutItemService) { }

  ngOnInit(): void {
      this.getItems();
      
    }
  getItems(): void {
    this.service.getAllItems()
      .subscribe(items => this.items = items);
    this.service.getSum().subscribe(sum => this.itemSum = sum);
  }
  
}
