import { Injectable } from '@angular/core';
import { Item } from './item.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient) { 
}
  readonly _baseUrl = "https://localhost:4200/api/Item";
  formData: Item = new Item();
  list: Item[];

  postMember() {
    return this.http.post(this._baseUrl, this.formData);
  }
  putItem() {
    return this.http.put(this._baseUrl+"/"+this.formData.id,this.formData);
  }
  deleteItem(id: number)
  {
    return this.http.delete(this._baseUrl+"/"+id);
  }
  /*
  refreshList() {
    this.http.get(this._baseUrl)
      .toPromise()
      .then(res => this.list = res as Item[]);
  }
  */
 }
