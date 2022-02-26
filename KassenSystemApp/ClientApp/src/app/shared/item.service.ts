import { Injectable } from '@angular/core';
import { Item } from './item.model';
import { HttpBaseService } from './http-base.service';
import { BehaviorSubject, catchError, map, Observable, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ItemService {


  public itemlist: Item[] = [];
  public cItems = new BehaviorSubject(this.itemlist);
  public items = this.cItems.asObservable();

  constructor(private httpBase: HttpBaseService){}
  readonly endpoint = "item";

  private log(message: string) {
    console.log(`ItemService: ${message}`);
  }
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); 
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
  setAllItems(itemList: Item[]):void
  {
    this.cItems.next(itemList);
    this.httpBase.post<Item[]>(this.endpoint + "/set", itemList).subscribe();
  }
  getItem(id: number)
  {
    return this.httpBase.get<Item[]>(this.endpoint+"/"+id)
      .pipe(
        map(heroes => heroes[0]), // returns a {0|1} element array
        tap(h => {
          const outcome = h ? 'fetched' : 'did not find';
          this.log(`${outcome} item id=${id}`);
        }),
        catchError(this.handleError<Item>(`getItem id=${id}`))
      );
  }
  
  
  
 }
