import { Component, OnInit } from '@angular/core';
import { CheckoutItem } from '../../shared/checkout-item.model';

@Component({
  selector: 'app-customer-display',
  templateUrl: './customer-display.component.html',
  styles: [
  ]
})
export class CustomerDisplayComponent implements OnInit {
  items: CheckoutItem[] = [
    { id: 11, name: "Nina",itemid: 1, pricefull: 4, pricesingle: 100, amount: 1 },
    { id: 12, name: "gdsa", itemid: 2, pricefull: 1200, pricesingle: 100, amount: 1 },
    { id: 13, name: "sdg<", itemid: 3, pricefull: 23, pricesingle: 100, amount: 0 },
    { id: 14, name: "bsb", itemid: 4, pricefull: 100, pricesingle: 100, amount: 1 },
    { id: 15, name: "sbf", itemid: 5, pricefull: 331, pricesingle: 100, amount: 1 },
    { id: 16, name: "sb", itemid: 6, pricefull: 100, pricesingle: 100, amount: 1 }
  ];
  itemSum: number = this.items.map(a => a.pricefull).reduce(function (a, b) {
    return a + b;
  });
 
  constructor() { }

  ngOnInit(): void {
  }

}
