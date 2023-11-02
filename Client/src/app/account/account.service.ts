import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { AuthResultDto } from '../dtos/responses/auth-result.dto';
import { UserDto } from '../dtos/responses/user.dto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.baseUrl;

  private currentAuth = new ReplaySubject<AuthResultDto | null>(1);
  currentAuth$ = this.currentAuth.asObservable();

  constructor(private httpClient: HttpClient) {
  }

  login(login: any) {
    return this.httpClient.post<AuthResultDto>(this.baseUrl + '/account/login', login).pipe(map(authResult => {
      sessionStorage.setItem('token', authResult.token);
      this.currentAuth.next(authResult);
      return authResult;
    }));
  }

  register(data: any) {
    return this.httpClient.post<AuthResultDto>(this.baseUrl + '/account/register', data).pipe(map(authResult => {
      sessionStorage.setItem('token', authResult.token);
      this.currentAuth.next(authResult);
      return authResult;
    }));
  }

  logout() {
    sessionStorage.removeItem('token');
    this.currentAuth.next(null);
  }

  loadCurrentAuth(): Observable<UserDto | null> {
    const token = sessionStorage.getItem('token');
    if (!token) {
      this.currentAuth.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.append('authorization', 'Bearer ' + token);

    return this.httpClient.get<UserDto>(this.baseUrl + '/account', { headers }).pipe(map((user): UserDto => {
      const authResult = {
        email: user.email,
        name: user.name,
        token: token
      };
      this.currentAuth.next(authResult);
      return user;
    }))
  }

  isEmailUnique(email: string) {
    return this.httpClient.get<boolean>(this.baseUrl + '/account/isEmailUnique?email=' + email);
  }

  updateUserAddress(address: any) {
    return this.httpClient.put(this.baseUrl + '/account/address', address);
  }
}
