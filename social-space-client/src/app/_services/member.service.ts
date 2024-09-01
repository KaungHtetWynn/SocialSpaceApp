import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { AccountService } from './account.service';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MemberService {
  private _http = inject(HttpClient); // function version of injection

  private _accountService = inject(AccountService); // get token

  baseUrl = environment.apiUrl;

  //Caching data in Angular services – Using the service to store state
  members = signal<Member[]>([]);

  constructor() { }

  getMembers() {
    // need to provide options because of [Authorize] Authentication attribute, so pass token
    // return this._http.get<Member[]>(this.baseUrl + 'users', this.getHttpOptions());

    // You don't need to include bearer token because it is added in jwtbearer interceptor
    //return this._http.get<Member[]>(this.baseUrl + 'users');

    return this._http.get<Member[]>(this.baseUrl + 'users').subscribe({
      next: (response) => {
        this.members.set(response);
      }
    });
  }

  // username is unique so retrieving it using username is ok
  getMember(username: string) {

    // return signal member if it is no undefined
    // You can comment out and test, by doing spinner will be gone
    // because of you do not need to make a request to api (you are retrieving the member persisted in service)
    const member = this.members().find(x => x.userName === username);
    if(member !== undefined) {
      return of(member);
    }


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



  // We also need to update signal array with new info
  // so that anything listening to this signal can react to the update
  // so we are using pipe (you can test by removing pipe)
  // 
  updateMember(member: Member) {
    // returns Observable of the response as a JavaScript object
    //return this._http.put(this.baseUrl + 'users', member);


    // by using tap its update the value of the signal and it's notify dependents
    // so anybody that's listening to the signal will be nofified of changed
    // And we don't need to go out and load the member from our API when we're viewing this inside our browser.

    return this._http.put(this.baseUrl + 'users', member).pipe(
      tap(() => {
        this.members.update(members => members.map(m => m.userName === member.userName ? member : m))
      })
    );
  }


}
