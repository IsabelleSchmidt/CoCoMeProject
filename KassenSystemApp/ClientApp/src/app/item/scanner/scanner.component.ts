import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { CheckoutItem } from '../../shared/checkout-item.model';
import { CheckoutItemService } from '../../shared/checkout-item.service';
import { Item } from '../../shared/item.model';
import { ItemService } from "../../shared/item.service";

import { PaymentMethod } from '../../shared/sale.model';
import { SaleService } from "../../shared/sale.service";
import { StoreServerService } from '../../shared/store-server.service';

@Component({
  selector: 'app-scanner',
  templateUrl: './scanner.component.html',
  styles: [
  ]
})
export class ScannerComponent implements OnInit {
 
  

  constructor(public service: ItemService, public storeservice: StoreServerService, public checkoutservice: CheckoutItemService, public saleservice: SaleService) {
    
  }
  itemSubsription: Subscription;
  items: Item[] = [];
  expressSubsription: Subscription;
  express: boolean = false;
  cItemSubsription: Subscription;
  checkoutItems: CheckoutItem[] = [];

  ngOnInit(): void {
    this.getItems();
    this.itemSubsription = this.service.items.subscribe(list => this.items = list);
    this.expressSubsription = this.saleservice.isExpress.subscribe(ex => this.express = ex);
    this.cItemSubsription = this.checkoutservice.checkoutItemsTest.subscribe(list => this.checkoutItems = list);
  }
  getItems(): void {
    this.storeservice.getAllItems().subscribe(i => this.service.setAllItems(i));
      
  }
 
  onSelectItem(item: Item): void {
    console.log(this.express + " : " + this.checkoutItems.length);
    if (this.express) {
      var i = 0;
      this.checkoutItems.forEach(coi => i += coi.amount);
      if (i < 8) {
        this.checkoutservice.addItem(item);
      }
    } else {
      this.checkoutservice.addItem(item);
    }
    
    
   
  }
  
}
