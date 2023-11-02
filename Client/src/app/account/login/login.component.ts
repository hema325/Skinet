import { Component, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {


  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required]
  })

  constructor(private accountService: AccountService,
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private router: Router,
    private toastrService: ToastrService) { }

  login() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: response => {
        let returnUrl = this.activatedRouter.snapshot.queryParamMap.get('returnUrl');
        console.log(returnUrl);
        if (!returnUrl)
          returnUrl = '/home';
        this.router.navigateByUrl(returnUrl);
        this.toastrService.success('logged in successfully');
      }
    });
  }

}
