import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { Observable } from 'rxjs';
import { NgIf } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, NgIf, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  
  //private accountService: AccountService = inject(AccountService);
  // removing private - child class
  accountService: AccountService = inject(AccountService);

  // flag to show and remove nav content
  //loggedIn = false;

  //model: any = {}; // giving an empty object as initial value
  model: loginDto = {
    username: "",
    password: ""
  };

  login() {
    console.log(this.model)
    this.accountService.login(this.model).subscribe({
      next: response => {
        console.log(response);
      },
      error: error => console.log(error),
      complete: () => console.log('Request has completed')
    });
  }
  
  logout() {
    this.accountService.logout();
  }
}

class loginDto {
  username: string;
  password: string

  constructor(username: string, password: string) {
    this.username = username;
    this.password = password;
  }
}
