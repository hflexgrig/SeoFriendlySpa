import { Component, inject, OnInit } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';
import { ItemService } from '../services/item.service';
import { ActivatedRoute } from '@angular/router';
import { Item } from '../models/item';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.css'],
})
export class ItemsComponent implements OnInit {
  private metaService = inject(Meta);
  private titleService = inject(Title);
  private itemsService = inject(ItemService);
  private route = inject(ActivatedRoute);
  item: Item = { title: '', content: '' } as Item;
  id: number = 0;

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.id = params['id'];
      this.getAndUpdateItem();
    });
  }

  private getAndUpdateItem() {
    this.itemsService.getItem(this.id).subscribe({
      next: (value: Item) => {
        this.item = value;
        this.titleService.setTitle(this.item.title);

        this.metaService.updateTag({
          property: 'title',
          content: this.item.title,
        });
      },
      error: (err) => {
        console.error(err.error);
      },
    });
  }
}
