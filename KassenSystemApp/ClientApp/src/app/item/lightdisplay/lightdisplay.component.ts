import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SaleService } from '../../shared/sale.service';

@Component({
  selector: 'app-lightdisplay',
  templateUrl: './lightdisplay.component.html',
  styles: [
  ]
})
export class LightdisplayComponent implements OnInit {
  expressSubsription: Subscription;
  express: boolean = false;
  constructor(public saleService: SaleService) { }

  ngOnInit(): void {
    this.expressSubsription = this.saleService.isExpress.subscribe(ex => this.express = ex);
  }

}
