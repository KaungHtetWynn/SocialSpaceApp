import { Component, inject, OnInit } from '@angular/core';
import { MemberService } from '../../_services/member.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {

  memberService = inject(MemberService);
  //members: Member[] = []; // no longer need we will get this from signal in service
  // now we are using service to persist the array
  
  
  ngOnInit(): void {
    //this.loadAllMembers();

    // will only load if signal array doesn't have any members
    if(this.memberService.members().length === 0) {
      this.loadAllMembers();
    }
  }

  loadAllMembers() {
    // this.memberService.getMembers().subscribe({
    //   next: members => {
    //     this.members = members;
    //   }
    // })

    this.memberService.getMembers()
    
  }

}
