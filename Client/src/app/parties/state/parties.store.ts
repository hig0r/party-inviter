import { Injectable } from '@angular/core';
import { EntityState, EntityStore, StoreConfig } from '@datorama/akita';
import { Party, PartyDetailed } from './party.model';

export interface PartiesState extends EntityState<Party | PartyDetailed> {}

@Injectable({ providedIn: 'root' })
@StoreConfig({ name: 'parties' })
export class PartiesStore extends EntityStore<PartiesState> {
  constructor() {
    super();
  }
}
