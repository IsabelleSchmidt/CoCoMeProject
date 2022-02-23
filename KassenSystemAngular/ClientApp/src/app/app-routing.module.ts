import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerDisplayComponent } from "././item/customer-display/customer-display.component"
import { CashRegisterComponent } from './item/cash-register/cash-register.component';
import { CashboxComponent } from './item/cashbox/cashbox.component';
import { LightdisplayComponent } from './item/lightdisplay/lightdisplay.component';
import { NavigationComponent } from './item/navigation/navigation.component';
import { PrinterComponent } from './item/printer/printer.component';
import { ScannerComponent } from './item/scanner/scanner.component';

const routes: Routes = [
  { path: '', component: NavigationComponent },
  { path: 'cash-register', component: CashRegisterComponent },
  { path: 'customer-display', component: CustomerDisplayComponent },
  { path: 'printer', component: PrinterComponent },
  { path: 'lightdisplay', component: LightdisplayComponent },
  { path: 'scanner', component: ScannerComponent },
  { path: 'cashbox', component: CashboxComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
