import { Component, OnInit } from '@angular/core';
import { PartiesService } from '../state/parties.service';
import { PartiesQuery } from '../state/parties.query';
import { ActivatedRoute } from '@angular/router';
import { PartyDetailed } from '../state/party.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-party',
  templateUrl: './party.component.html',
  styleUrls: ['./party.component.scss'],
})
export class PartyComponent implements OnInit {
  party$!: Observable<PartyDetailed>;

  constructor(
    private partiesService: PartiesService,
    private partiesQuery: PartiesQuery,
    private route: ActivatedRoute
  ) {
  }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.party$ = this.partiesQuery.selectEntity(id) as any;
    this.partiesService.get(id).subscribe();
  }
}
