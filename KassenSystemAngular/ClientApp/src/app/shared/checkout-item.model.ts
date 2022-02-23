export class CheckoutItem {
  id: number;
  itemid: number;
  name: string;
  pricesingle: number;
  pricefull: number;
  amount: number;
  constructor() {
    this.id = 0;
    this.itemid = 0;
    this.name = "";
    this.pricesingle = 0;
    this.pricefull = 0;
    this.amount = 0;
  }
}
