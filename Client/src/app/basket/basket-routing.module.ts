import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BasketComponent } from './basket.component';


const routes = [
  { path: '', component: BasketComponent, data: { breadcrumb: 'basket' } }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class BasketRoutingModule { }
