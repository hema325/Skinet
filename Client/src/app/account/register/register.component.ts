import { Component } from '@angular/core';
import { AbstractControl, AsyncValidatorFn, FormBuilder, ValidationErrors, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { password } from 'src/app/validators/password.validator';
import { compareTo } from 'src/app/validators/equal.validator';
import { Observable, debounceTime, delay, finalize, map, switchMap, take } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  errors: string[] = [];
  passwordPattern = '(?=(.*[0-9]))(?=.*[^a-zA-Z0-9])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{6,}';

  registerForm = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(100)]],
    email: ['', [Validators.required, Validators.email], [this.emailUnique()]],
    password: ['', [Validators.required, Validators.minLength(6), password()]],
    confirmPassword: ['', [Validators.required, compareTo('password')]]
  });

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private toastrService: ToastrService) { }

  register() {
    this.accountService.register(this.registerForm.value).subscribe({
      error: error => this.errors = error.errors,
      complete: () => {
        this.router.navigateByUrl('/home');
        this.toastrService.success('registered successfully');
      }
    });
  }

  emailUnique(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<ValidationErrors | null> => {
      return control.valueChanges.pipe(debounceTime(500),
        take(1),
        switchMap(() => this.accountService
          .isEmailUnique(control.value)
          .pipe(map(result => result ? null : { 'emailUnique': 'false' }),
            finalize(() => control.markAsTouched()))));
    }
  }

}
