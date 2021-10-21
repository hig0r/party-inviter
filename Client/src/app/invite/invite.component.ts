import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InviteService } from './invite.service';
import { Invitation } from './invitation.model';
import { WillAttend } from '../parties/state/party.model';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.scss'],
})
export class InviteComponent implements OnInit {
  invitation?: Invitation;
  hashId!: string;

  constructor(
    private route: ActivatedRoute,
    private inviteService: InviteService
  ) {}

  ngOnInit(): void {
    this.hashId = this.route.snapshot.paramMap.get('hashId')!;
    this.inviteService.get(this.hashId).subscribe((res) => {
      this.invitation = res;
    });
  }

  answer(willAttend: WillAttend): void {
    this.inviteService.answer(this.hashId, willAttend).subscribe((_) => {
      this.invitation!.alreadyAnswered = true;
    });
  }
}
