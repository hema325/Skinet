import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    isAuthenticating = true;

    constructor(private accountService: AccountService) { }

    ngOnInit(): void {
        this.accountService.loadCurrentAuth().subscribe({
            complete: () => this.isAuthenticating = false,
            error: error => this.isAuthenticating = false,
        });
    }

}
