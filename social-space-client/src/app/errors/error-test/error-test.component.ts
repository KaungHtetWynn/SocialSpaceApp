import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { map } from 'rxjs';

@Component({
  selector: 'app-error-test',
  standalone: true,
  imports: [],
  templateUrl: './error-test.component.html',
  styleUrl: './error-test.component.css'
})
export class ErrorTestComponent {

  // we are using it directly normally we use in service
  http = inject(HttpClient);
  baseUrl =  'http://localhost:5000/api/';
  validationErrors: string[] = [];

  // 400 bad request error
  register_error() {
    this.http.post(this.baseUrl + 'account/register', {}).subscribe({
      next: (response) => {
        console.log(response)
      },
      complete: () => {},
      error: (error) => {
        // throw modalStateErrs.flat(); throw from interceptor will arrive here
        console.log(error)
        this.validationErrors = error; 
        // error the flattened array passed down through the pipeline from interceptor
        // so that you can also modify the error in this component as well.


      }
    });
  }

  // 400 bad request error
  bad_request() {
    this.http.get(this.baseUrl + 'error/bad-request').subscribe({
      next: (response) => {
        console.log(response)
      },
      complete: () => {},
      error: (error) => {
        console.log(error)
      }
    });
  }

  // 404 not found error
  not_found() {
    this.http.get(this.baseUrl + 'error/not-found').subscribe({
      next: (response) => {
        console.log(response)
      },
      complete: () => {},
      error: (error) => {
        console.log(error)
      }
    });
  }

  // 500 internal server error
  server_error() {
    this.http.get(this.baseUrl + 'error/server-error').subscribe({
      next: (response) => {
        console.log(response)
      },
      complete: () => {},
      error: (error) => {
        console.log(error)
      }
    });
  }

  // 401 unauthorized error
  auth_error() {
    this.http.get(this.baseUrl + 'error/auth-error').subscribe({
      next: (response) => {
        console.log(response)
      },
      complete: () => {},
      error: (error) => {
        console.log(error)
      }
    });
  }



}
