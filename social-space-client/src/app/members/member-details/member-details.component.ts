import { Component, inject, OnInit } from '@angular/core';
import { MemberService } from '../../_services/member.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../_models/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-member-details',
  standalone: true,
  imports: [TabsModule, GalleryModule],
  templateUrl: './member-details.component.html',
  styleUrl: './member-details.component.css'
})
export class MemberDetailsComponent implements OnInit {

  // access to member service
  private memberService = inject(MemberService);

  // access to route parameter
  private route = inject(ActivatedRoute);

  member?: Member;

  photos: GalleryItem[] = [];

  ngOnInit(): void {

    const username = this.route.snapshot.paramMap.get('username');

    if (!username) {
      return;
    }

    this.memberService.getMember(username).subscribe({
      next: member => {
        this.member = member;
        member.photos.map(p => {
          this.photos.push(new ImageItem({src: p.imageUrl, thumb: p.imageUrl}))
        })

      }
    })
  }
}
