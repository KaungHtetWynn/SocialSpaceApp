import { JsonPipe, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';
import { HomeComponent } from './home/home.component';

@Component({
    selector: 'app-root',
    standalone: true, // Can also import CommonModule instead of NgFor, We don't need NgFor If we use @for
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, NgFor, NavComponent, HomeComponent]
})
export class AppComponent implements OnInit {

  
  private accountService = inject(AccountService);
  title = 'Social Space App';
  //users: any;

  ngOnInit(): void {
    //this.getUsers();
    this.setCurrentUser();
  }


  // Key:user =	Value:{"userName":"john","token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"}
  setCurrentUser() {
    const userLocalString = localStorage.getItem('user');
    if(!userLocalString) return;

    // Convert JSON to JavaScript object
    const user = JSON.parse(userLocalString);
    this.accountService.currentUser.set(user);


  }

  


  
}