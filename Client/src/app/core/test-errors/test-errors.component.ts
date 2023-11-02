import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent {
  baseUrl = environment.baseUrl;
  constructor(private httpClient: HttpClient) { }

  errors: string[] = [];

  get404NotFound() {
    this.httpClient.get(this.baseUrl + '/buggy/notfound').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    });
  }

  get500InternalServerError() {
    this.httpClient.get(this.baseUrl + '/buggy/serverError').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    });
  }

  get401Unauthorized() {
    this.httpClient.get(this.baseUrl + '/buggy/unauthorized').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    });
  }

  get400BadRequest() {
    this.httpClient.get(this.baseUrl + '/buggy/badRequest').subscribe({
      next: response => console.log(response),
      error: error => this.errors = error.errors
    });
  }

  get500Exception() {
    this.httpClient.get(this.baseUrl + '/buggy/exception').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    });
  }

}
