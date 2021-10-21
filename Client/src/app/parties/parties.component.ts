import { Component, OnInit } from '@angular/core';
import { PartiesService } from './state/parties.service';
import { PartiesQuery } from './state/parties.query';

@Component({
  selector: 'app-parties',
  templateUrl: './parties.component.html',
  styleUrls: ['./parties.component.scss'],
})
export class PartiesComponent implements OnInit {
  parties$ = this.partiesQuery.selectAll();
  displayedColumns = ['name', 'description'];

  constructor(
    private partiesService: PartiesService,
    private partiesQuery: PartiesQuery
  ) {
  }

  ngOnInit(): void {
    this.partiesService.get().subscribe();
  }
}
