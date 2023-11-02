import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { ToastrModule } from 'ngx-toastr';
import { OrderSummaryComponent } from './order-summary/order-summary.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FormInputComponent } from './form-input/form-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { StepperComponent } from './stepper/stepper.component';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderSummaryComponent,
    FormInputComponent,
    StepperComponent,
    BasketSummaryComponent,
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    ToastrModule.forRoot(),
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    CdkStepperModule,
    RouterModule
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    ToastrModule,
    OrderSummaryComponent,
    BsDropdownModule,
    FormInputComponent,
    StepperComponent,
    CdkStepperModule,
    ReactiveFormsModule,
    BasketSummaryComponent
  ]
})
export class SharedModule {

}
