import { Component, Input, OnInit } from '@angular/core';
import { CheckoutItemService } from '../../shared/checkout-item.service';
import { ItemService } from '../../shared/item.service';
import { SaleService } from '../../shared/sale.service';
import { StoreServerService } from '../../shared/store-server.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styles: [
  ]
})
export class NavigationComponent implements OnInit {
  constructor(public checkoutService: CheckoutItemService, public saleService: SaleService, public itemService: ItemService, public storeService: StoreServerService) { }

  ngOnInit(): void {
  }

}
