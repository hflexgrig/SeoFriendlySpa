import { HttpClient } from '@angular/common/http';
import {Inject, inject, Injectable} from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item';
import { of} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  private url = `${environment.apiurl}items/`
  private http = inject(HttpClient);

  constructor(@Inject("itemData") private data: any) {
  }

  getItem(id: number){
    if(this.data && this.data.id == id){
      const cloned = this.data;
      //once data is read from server, we don't need it anymore!
      this.data = undefined;

      return of(cloned);
    }

    return this.http.get<Item>(`${this.url}${id}`);
  }
}
