export class CheckoutItem {
  id: number;
  itemId: number;
  name: string;
  priceSingle: number;
  priceFull: number;
  amount: number;
  constructor() {
    //this.id = 0;
    this.itemId = 0;
    this.name = "";
    this.priceSingle = 0;
    this.priceFull = 0;
    this.amount = 0;
  }
}
