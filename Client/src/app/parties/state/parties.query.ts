import { Injectable } from '@angular/core';
import { QueryEntity } from '@datorama/akita';
import { PartiesStore, PartiesState } from './parties.store';

@Injectable({ providedIn: 'root' })
export class PartiesQuery extends QueryEntity<PartiesState> {

  constructor(protected store: PartiesStore) {
    super(store);
  }

}
