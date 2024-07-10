import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  // 1st way - dependency injection using inject method
  // private http2 = inject(HttpClient);

  // 2nd way - dependency injection using constructor
  private _http;
  baseUrl = 'http://www.localhost:5000/api/';
  currentUser = signal<User | null>(null);

  constructor(http: HttpClient) {
    this._http = http;
  }

  
  // login(model: any) {
  //   return this._http.post(this.baseUrl + 'account/login', model);
  // }

  // pass the model as body in a post request
  login(model: any) {
    return this._http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map(user => {
        if(user) {
          // Create a JSON string from a JavaScript object.
          localStorage.setItem('user', JSON.stringify(user));
          

          this.currentUser.set(user);
          console.log('account service login:' + user.userName + " & " + user.token);
          //console.log('MyLog:' + localStorage.getItem('user'));
        }
      })
    );
  }

  // pass register model to server via post request

  register(model: any) {
    return this._http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        }
        // return the projected value if not we are returning 'return this._http.post'
        return user;
      })
    );
  }

  logout() {
    localStorage.removeItem('user'); // remove user obj from local storage - UserDto API
    this.currentUser.set(null); // set signal property to null
  }
}
