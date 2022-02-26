import { Injector, ModuleWithProviders, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomerDisplayComponent } from './item/customer-display/customer-display.component';

import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ItemComponent } from './item/item.component';
import { PrinterComponent } from './item/printer/printer.component';
import { NavigationComponent } from './item/navigation/navigation.component';
import { CashRegisterComponent } from './item/cash-register/cash-register.component';
import { ScannerComponent } from './item/scanner/scanner.component';
import { CashboxComponent } from './item/cashbox/cashbox.component';
import { LightdisplayComponent } from './item/lightdisplay/lightdisplay.component';
import { ItemService } from './shared/item.service';
import { SaleService } from './shared/sale.service';
import { CheckoutItemService } from './shared/checkout-item.service';
import { CardreaderComponent } from './item/cardreader/cardreader.component';


export let AppInjector: Injector;

@NgModule({
  declarations: [
    AppComponent,
    CustomerDisplayComponent,
    ItemComponent,
    PrinterComponent,
    NavigationComponent,
    CashRegisterComponent,
    ScannerComponent,
    CashboxComponent,
    LightdisplayComponent,
    CardreaderComponent
  ],
  imports: [
    FormsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule
  ],
  providers: [ItemService, SaleService, CheckoutItemService, ItemService],
  bootstrap: [AppComponent]
})
export class AppModule {
  
}
