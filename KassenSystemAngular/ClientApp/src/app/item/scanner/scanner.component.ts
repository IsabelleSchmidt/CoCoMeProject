import { Component, OnInit } from '@angular/core';
import { Item } from '../../shared/item.model';
import { ItemService } from "../../shared/item.service";

@Component({
  selector: 'app-scanner',
  templateUrl: './scanner.component.html',
  styles: [
  ]
})
export class ScannerComponent implements OnInit {
  items: Item[] =[
    { id: 11, name:"Nina", price: 100, amount: 1 },
  { id: 12, name:"gdsa", price: 55, amount: 1 },
  { id: 13, name:"sdg<", price: 4, amount: 0 },
  { id: 14, name:"bsb", price: 79, amount: 1 },
  { id: 15, name:"sbf", price: 222, amount: 1 },
  { id: 16, name:"sb", price: 552, amount: 1 }
  ];

  selectedItem?: Item;
  constructor(public service: ItemService) { }

  ngOnInit(): void {
  }
  onSelectItem(item: Item): void {
    this.selectedItem = item;
  }
}
