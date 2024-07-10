import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from '../register/register.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  ngOnInit(): void {
    this.getUsers();
  }
  http = inject(HttpClient) // New way (last time we use constructor injection)
  showRegisterForm : boolean = false;
  users: any;

  registerFormToggle() {
    this.showRegisterForm = !this.showRegisterForm;
  }

  hideRegisterForm(event: boolean) {
    this.showRegisterForm = event;
  }

  getUsers() {
    this.http.get('http://localhost:5000/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed')
    })
  }
}
