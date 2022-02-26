import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SaleService } from '../../shared/sale.service';

@Component({
  selector: 'app-cardreader',
  templateUrl: './cardreader.component.html',
  styles: [
  ]
})
export class CardreaderComponent implements OnInit {

  cardSubscription: Subscription;
  awaitCard: boolean = false;
  constructor(public saleService: SaleService) { }

  ngOnInit(): void {

    this.cardSubscription = this.saleService.awaitCard.subscribe(ac => this.awaitCard = ac);
  }
  acceptCard(): void {
    if (this.awaitCard) this.saleService.acceptCard(true);
  }

}
