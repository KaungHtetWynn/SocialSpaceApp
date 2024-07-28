import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { Observable } from 'rxjs';
import { NgIf, TitleCasePipe } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse, HttpResponseBase } from '@angular/common/http';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, NgIf, BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  
  //private accountService: AccountService = inject(AccountService);
  // removing private - child class
  accountService: AccountService = inject(AccountService);
  router: Router = inject(Router);
  toastr: ToastrService = inject(ToastrService);

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
      next: () => {
        this.router.navigateByUrl('/members');
      },
      // error is the surrounding object and our message is contained inside a property called err
      // use error property of err object to retrieve the error message
      error: err => {
        this.toastr.error(err.error);
        console.log(err)
      },
      complete: () => console.log('Request has completed')
    });
  }
  // next: response => {
  //   console.log(response);
  // },
  
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
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
