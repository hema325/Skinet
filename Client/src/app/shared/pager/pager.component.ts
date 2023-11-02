import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.css']
})
export class PagerComponent {
  @Input() totalPages!: number;
  @Input() pageSize!: number;
  @Output() pageChanged = new EventEmitter<number>();

  onPageChanged(event: any) {
    this.pageChanged.emit(event.page);
  }
}
