import { Component, OnInit } from '@angular/core';
import { CashRegisterService } from '../../shared/cash-register.service';
import { CheckoutItem } from '../../shared/checkout-item.model';

@Component({
  selector: 'app-cash-register',
  templateUrl: './cash-register.component.html',
  styles: [
  ]
})
export class CashRegisterComponent implements OnInit {

  constructor(public service: CashRegisterService) { }
  isExpress: boolean = false;
  sum: number = 0;

  receiveId: boolean = false;
  receivePayment: boolean = false;
  inputText: string = "";
  items: CheckoutItem[] = [];

  fieldInputDisabled: boolean =true;
  endInputDisabled: boolean =true;
  expressInputDisabled: boolean =true;
  cashInputDisabled: boolean =true;
  cardInputDisabled: boolean =true;
  payInputDisabled: boolean =true;
  itemidInputDisabled: boolean =true;
  itemplusInputDisabled: boolean =true;
  newInputDisabled: boolean =true;
  deleteInputDisabled: boolean =true;
  okInputDisabled: boolean =true;
  keyInputDisabled: boolean =true;


  ngOnInit(): void {
    this.expressInputDisabled = false;//check if in express mode
    this.newInputDisabled = false;//check if middle of sale
    this.service.deleteExpiredSales();
    this.inputText = "";
  }
  onNewSale(): void {
    console.log("newSale");
    this.itemplusInputDisabled =false;
    this.payInputDisabled =false;
    this.itemidInputDisabled =false;
    this.deleteInputDisabled =false;
    this.newInputDisabled =true;
    this.expressInputDisabled =true;
    this.service.getAllItems()
      .subscribe(items => this.items = items);
  }
  onPay(): void {
    //service check if express, get sum
    this.service.getExpress().subscribe(expr => this.isExpress = expr);
    if (!this.isExpress) {

      this.cardInputDisabled = false;
    }
    this.itemplusInputDisabled =true;
    this.payInputDisabled =true;
    this.itemidInputDisabled =true;
    this.deleteInputDisabled =true;
    this.cashInputDisabled =false
  }
  onItemPlus(): void {
    //service plus one if not empty
    this.service.itemPlusOne();
  }
  onDelete(): void {
    //service delete if not empty
    this.service.deleteLast();
  }
  onItemId(): void {
    this.itemidInputDisabled =true;
    this.keyInputDisabled =false;
    this.okInputDisabled =false;
    this.receiveId =true;
    this.itemplusInputDisabled =true;
    this.payInputDisabled =true;
    this.itemidInputDisabled =true;
    this.deleteInputDisabled =true;
  }
  onOk(): void {
    if (this.receiveId) {
      //service add item if id ok
      this.service.addById(+this.inputText);
      this.receiveId = false;
      this.itemplusInputDisabled = false;
      this.payInputDisabled = false;
      this.itemidInputDisabled = false;
      this.deleteInputDisabled = false;
      this.keyInputDisabled = true;
      this.okInputDisabled = true;
      this.inputText = "";
    } else if (this.receivePayment && (this.inputText !== "")) {
      //calc input > sum
      this.service.getSum().subscribe(s => this.sum = s);
      this.service.getSum().subscribe(i => console.log(i + ": " + this.inputText) );
      if (this.service.getSum().subscribe(i => i < (+this.inputText*100) ) ){
        this.receivePayment = false;
        this.endInputDisabled = false;

        this.keyInputDisabled = true;
        this.okInputDisabled = true;
        //calculate cash back from input - sum
        this.service.getSum().subscribe(i => this.inputText = String((+this.inputText) - (i/100)));
      }
      
    }
   
    
  }
  onCard(): void {
    this.cardInputDisabled =true;
    this.cashInputDisabled = true;
    this.endInputDisabled = false;
    //activate card service somehow
    if (!this.isExpress) {
      //cardservice
    }
  }

  onCash(): void {
    this.receivePayment = true;
    this.cashInputDisabled =true;
    this.cardInputDisabled =true;
    this.okInputDisabled =false;
    this.keyInputDisabled =false;
    //activate cashbox somehow
  }
  onEnd(): void {

    //service clear list, send list to store, check express
    //this.service.finishSale();
    this.service.clear();
    this.service.deleteExpiredSales();
    this.expressInputDisabled =false;
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
    this.expressInputDisabled =true;
    //service end express (dont forgest list length and card/cash)
  }

  getCashBack(): string {
    //service get sum
    var y: number = +this.inputText;
    console.log(y);
    //return y-(sum/100);
    return "";
  }
}
