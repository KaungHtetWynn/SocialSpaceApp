import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  private _http = inject(HttpClient); // function version of injection

  private _accountService = inject(AccountService); // get token

  baseUrl = environment.apiUrl;

  constructor() { }

  getMembers() {
    // need to provide options because of [Authorize] Authentication attribute, so pass token
    // return this._http.get<Member[]>(this.baseUrl + 'users', this.getHttpOptions());

    // You don't need to include bearer token because it is added in jwtbearer interceptor
    return this._http.get<Member[]>(this.baseUrl + 'users');
  }

  getMember(username: string) {
    // return this._http.get<Member>(this.baseUrl + 'users/' + username, this.getHttpOptions());
    return this._http.get<Member>(this.baseUrl + 'users/' + username);
  }


  // You don't need to include bearer token because it is added in jwtbearer interceptor
  // includes the bearer token in the Authorization header of every request it makes to the server.
  // currentUser is Angular Signal so must use () to access
  // getHttpOptions() {
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: `Bearer ${this._accountService.currentUser()?.token}`
  //     })
  //   }
  // }


}
