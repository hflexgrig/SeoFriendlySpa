import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ItemsComponent } from './items/items.component';

// function getCustomData() {
//   return '';
// }

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ItemsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'item/:id', component: ItemsComponent },
    ])
  ],
  providers: [
    //@ts-ignore
    {provide: "customData", useValue: executeFunction("getCustomData")},
    //@ts-ignore
    {provide: "itemData", useValue: executeFunction("getItemData")}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

function executeFunction(funcName:string){

  return  eval('typeof ' + funcName) === 'function' ? eval(funcName)() : "";
}
