import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(public basketService: BasketService,
    public accountService: AccountService,
    public router: Router,
    private toastrService: ToastrService) { }

  ngOnInit(): void {
    console.log(this.router);
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/home');
    this.toastrService.success('logged out successfully');
  }
}
