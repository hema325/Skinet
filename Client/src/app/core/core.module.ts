import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { TestErrorsComponent } from './test-errors/test-errors.component';
import { NotfoundErrorComponent } from './notfound-error/notfound-error.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    NavBarComponent,
    TestErrorsComponent,
    NotfoundErrorComponent,
    ServerErrorComponent,
    SectionHeaderComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    BreadcrumbModule,
    NgxSpinnerModule,
    SharedModule
  ],
  exports: [
    NavBarComponent,
    SectionHeaderComponent,
    NgxSpinnerModule
  ]
})
export class CoreModule { }
