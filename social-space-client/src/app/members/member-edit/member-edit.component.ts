import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { Member } from '../../_models/member';
import { HttpClient } from '@angular/common/http';
import { AccountService } from '../../_services/account.service';
import { MemberService } from '../../_services/member.service';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {

  member?: Member;
  // template is a child of component
  // access template reference variable
  // component is initialized first and then the view the template is initialized
  // that's why ?
  @ViewChild('editFormTrv') editFormTemRef?: NgForm;

  // For browser
  // For example Use to prevent navigating using browser control like back button and home button when there are unsaved changes in form
  // Broser event that we need to get
  // Access browser event from Angular component
  @HostListener('window:beforeunload', ['$event']) notify($event:any) {
    if(this.editFormTemRef?.dirty) {
      $event.returnValue = true;
    }
  }

  private _accountService = inject(AccountService);
  private _memberService = inject(MemberService);
  private _toastrService = inject(ToastrService);

  // implement OnInit because needs to get data
  ngOnInit(): void {
    this.retrieveMember();
    
  }

  retrieveMember() {

    const userName = this._accountService.currentUser()?.userName;

    if(!userName) {
      return;
    }
    
    this._memberService.getMember(userName).subscribe({
      next: (response) => {
        this.member = response;
      }
    });
  }

  updateMemberInformation() {
    //console.log(this.member);

      this._memberService.updateMember(this.editFormTemRef?.value).subscribe({
        next: (response) => {
          this._toastrService.success('Profile updated successfully!')

          // reset dirty after updating
          this.editFormTemRef?.reset(this.member);
        }
      })
    
    



    
  }
  

}
