import { Component } from '@angular/core';
import { RegisterService } from './register.service';
import * as myGlobal from './global'; //<==== this one (**Updated**)

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CoCoMe Cash Register';
  constructor(public service: RegisterService) { }
  
  ngOnInit(): void {
   
    
  }
}
