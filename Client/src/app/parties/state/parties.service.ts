import { Injectable } from '@angular/core';
import { NgEntityService } from '@datorama/akita-ng-entity-service';
import { PartiesStore, PartiesState } from './parties.store';

@Injectable({ providedIn: 'root' })
export class PartiesService extends NgEntityService<PartiesState> {

  constructor(protected store: PartiesStore) {
    super(store);
  }

}
