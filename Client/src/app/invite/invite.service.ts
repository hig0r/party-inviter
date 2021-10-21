import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Invitation } from './invitation.model';
import { WillAttend } from '../parties/state/party.model';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class InviteService {
  constructor(private http: HttpClient) {
  }

  get(hashId: string) {
    return this.http.get<Invitation>(`/api/invites/${ hashId }`);
  }

  answer(hashId: string, willAttend: WillAttend) {
    return this.http.post('/api/invites', { hashId, willAttend }).pipe(tap());
  }
}
