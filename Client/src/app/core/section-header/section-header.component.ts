import { Component } from '@angular/core';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.css']
})
export class SectionHeaderComponent {
  title?: string;
  constructor(private bcService: BreadcrumbService) {
    bcService.breadcrumbs$.subscribe(breadcrumbs => this.title = breadcrumbs[breadcrumbs.length - 1].label as string);
  }
}
