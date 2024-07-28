import { Component, EventEmitter, inject, input, Input, output, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  toastr = inject(ToastrService);
  //@Input() usersFromHomeComponent: any;

  // required = compiler support
  // usersFromHomeComponent = input.required<any>();

  // emits event from child component
  // You can be explicity about type
  //@Output() cancelRegistration: EventEmitter<boolean>  = new EventEmitter();
  //cancelRegistration = output(); // no need event emitter
  cancelRegistration = output<boolean>(); // emiting boolean

  cancel() {
    this.cancelRegistration.emit(false);
  }


  model: Register = {
    username: "",
    password: ""
  };

  register() {
    this.accountService.register(this.model).subscribe({
      next: response => {

        //
        console.log(response);
        this.cancel;
      },
      error: err => {
        this.toastr.error(err.error);
        console.log(err)
      }
      
    });
  }

  

  // onChangePrint(e: any) {
  //   console.log(e.target.value);
  // }

 
}

export interface Register{
  username: string;
  password: string
}
