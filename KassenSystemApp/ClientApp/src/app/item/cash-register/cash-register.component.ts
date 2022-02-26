import { Component, OnInit } from '@angular/core';
import { CheckoutItem } from '../../shared/checkout-item.model';
import { PaymentMethod } from '../../shared/sale.model';
import { SaleService } from '../../shared/sale.service';

import { CheckoutItemService } from '../../shared/checkout-item.service';
import { ItemService } from '../../shared/item.service';
import { StoreServerService } from '../../shared/store-server.service';
import { AppInjector } from '../../app.module';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-cash-register',
  templateUrl: './cash-register.component.html',
  styles: [
  ]
})
export class CashRegisterComponent implements OnInit {

  constructor(public saleService: SaleService, public checkoutService: CheckoutItemService, public itemService: ItemService, public storeService: StoreServerService) { }
 
  sumSubscription: Subscription;
  sumTotal = 0;
  expressSubsription: Subscription;
  express: boolean = false;
  cItemSubsription: Subscription;
  checkoutItems: CheckoutItem[] = [];

  cashboxSubscription: Subscription;
  openCashbox: boolean = false;
  cardSubscription: Subscription;
  awaitCard: boolean = false;


  receiveId: boolean = false;
  receivePayment: boolean = false;
  inputText: string = "";

  fieldInputDisabled: boolean =true;
  endInputDisabled: boolean =true;
  cashInputDisabled: boolean =true;
  cardInputDisabled: boolean =true;
  payInputDisabled: boolean =true;
  itemidInputDisabled: boolean =true;
  itemplusInputDisabled: boolean =true;
  newInputDisabled: boolean =true;
  deleteInputDisabled: boolean =true;
  okInputDisabled: boolean =true;
  keyInputDisabled: boolean =true;


  init(any: boolean) {
    if (any) {
      this.onNewSale();
    } else {
      this.newInputDisabled = false;
      this.saleService.deleteExpiredSales();
      this.inputText = "";
    }
  }
  
  ngOnInit(): void {
    this.checkoutService.getAllItems();
    this.saleService.getExpress();
    this.sumSubscription = this.checkoutService.sum.subscribe(message => this.sumTotal = message);
    this.expressSubsription = this.saleService.isExpress.subscribe(ex => this.express = ex);
    this.cItemSubsription = this.checkoutService.checkoutItemsTest.subscribe(list => this.checkoutItems = list);
    this.cashboxSubscription = this.saleService.openCash.subscribe(oc => this.openCashbox = oc);
    this.cardSubscription = this.saleService.awaitCard.subscribe(ac => this.awaitCard = ac);

    this.checkoutService.getList().subscribe(list => this.init(list.length > 0));

    

  }
  onNewSale(): void {
    this.storeService.getAllItems().subscribe(i => this.itemService.setAllItems(i));
    this.saleService.startSale();
    this.payInputDisabled = false;
    this.itemplusInputDisabled = false;
    this.itemidInputDisabled = false;
    this.deleteInputDisabled = false;
    this.newInputDisabled = true;
  }
  onPay(): void {
    if (this.checkoutItems.length > 0) {

      this.saleService.toPayment();
      this.itemplusInputDisabled = true;
      this.payInputDisabled = true;
      this.itemidInputDisabled = true;
      this.deleteInputDisabled = true;
      this.cashInputDisabled = false
      if (!this.express) {
        this.cardInputDisabled = false;
      }
    }
    
  }
  onItemPlus(): void {
    //service plus one
    if (this.express) {
      var i = 0;
      this.checkoutItems.forEach(coi => i += coi.amount);
      if (i < 8) {
        this.checkoutService.itemPlusOne();
      }
    } else {
      this.checkoutService.itemPlusOne();
    }
  }
  onDelete(): void {
    //service delete 
    if (this.checkoutItems.length > 0)
    {

    this.checkoutService.deleteLast();

    }
  }
  onItemId(): void {
    if (this.saleService.isExpress) {
      var i = 0;
      this.checkoutItems.forEach(coi => i += coi.amount);
      if (i < 8) {
        this.itemidInputDisabled = true;
        this.keyInputDisabled = false;
        this.okInputDisabled = false;
        this.receiveId = true;
        this.itemplusInputDisabled = true;
        this.payInputDisabled = true;
        this.itemidInputDisabled = true;
        this.deleteInputDisabled = true;
      }
    } else {
      this.itemidInputDisabled = true;
      this.keyInputDisabled = false;
      this.okInputDisabled = false;
      this.receiveId = true;
      this.itemplusInputDisabled = true;
      this.payInputDisabled = true;
      this.itemidInputDisabled = true;
      this.deleteInputDisabled = true;
    }
  }
  onOk(): void {
    if (this.receiveId) {
      //service add item if id ok
      this.checkoutService.addById(+this.inputText);
      
      this.receiveId = false;
      this.itemplusInputDisabled = false;
      this.payInputDisabled = false;
      this.itemidInputDisabled = false;
      this.deleteInputDisabled = false;
      this.keyInputDisabled = true;
      this.okInputDisabled = true;
      this.inputText = "";
    } else if (this.receivePayment && (this.inputText !== "")) {
       if (this.sumTotal < (+this.inputText)) {
          this.receivePayment = false;
          this.endInputDisabled = false;

          this.keyInputDisabled = true;
          this.okInputDisabled = true;
         //calculate cash back from input - sum
         this.inputText = String((+this.inputText) - (this.sumTotal));
         this.saleService.closeCashbox(false);
         this.cashboxSubscription = this.saleService.openCash.subscribe(oc => this.endInputDisabled = oc);
      }
    }
  }

  
  onCard(): void {
    this.cardInputDisabled =true;
    this.cashInputDisabled = true;
    this.endInputDisabled = false;
    //activate card service somehow

    this.saleService.acceptCard(false);
    this.cardSubscription = this.saleService.awaitCard.subscribe(ac => this.endInputDisabled = ac);
  }

  onCash(): void {
    this.receivePayment = true;
    this.cashInputDisabled =true;
    this.cardInputDisabled =true;
    this.okInputDisabled =false;
    this.keyInputDisabled =false;
    
  }
  onEnd(): void {

    //service clear list, send list to store, check express
    this.storeService.removeItemsStore(this.checkoutItems);
    this.checkoutService.clear();
    this.saleService.getExpress();
    this.saleService.deleteExpiredSales();


    this.newInputDisabled = false;
    this.cashInputDisabled = true;
    this.cardInputDisabled = true;
    this.inputText = "";
   
    this.endInputDisabled =true;
    
  }
  onKey(key: string): void {
    this.inputText += key;
  }
  onRemoveLast(): void {
    if (this.inputText.length > 0) {
      this.inputText = this.inputText.slice(0, -1);
    }
  }
  onExpress(): void {
    this.saleService.stopExpress();
    //service end express (dont forgest list length and card/cash)
  }

  
}
