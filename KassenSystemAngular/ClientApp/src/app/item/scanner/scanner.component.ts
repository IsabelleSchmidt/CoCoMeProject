import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Item } from '../../shared/item.model';
import { ItemService } from "../../shared/item.service";

import { PaymentMethod, Sale } from '../../shared/sale.model';
import { SaleService } from "../../shared/sale.service";

@Component({
  selector: 'app-scanner',
  templateUrl: './scanner.component.html',
  styles: [
  ]
})
export class ScannerComponent implements OnInit {
 
  items: Item[] = [];
  selectedItem?: Observable<Item>;
 
  
  constructor(public service: ItemService) {
    
  }

  ngOnInit(): void {
    this.getItems();
  }
  getItems(): void {
    this.service.getAllItems()
      .subscribe(items => this.items = items);
  }
  onSelectItem(item: Item): void {
    this.service.addItem(item);//.subscribe();
   
  }
  
}
