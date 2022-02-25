export class Sale {
  id: number;
  paymentMethod: PaymentMethod;
  datetime: Date;
  constructor() {
    this.id = 0;
    this.paymentMethod = PaymentMethod.Card;
    this.datetime = new Date();
  }
}

export enum PaymentMethod {
  Cash,
  Card
}
