import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Item } from '../models/item';

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  private url = `${environment.apiurl}items/`
  private http = inject(HttpClient);

  constructor() { }

  getItem(id: number){
    return this.http.get<Item>(`${this.url}${id}`);
  }
}
