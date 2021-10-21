export interface Party {
  id: number;
  name: string;
  description: string;
}

export interface PartyDetailed extends Party {
  invitationMessage: string;
  guests: Guest[];
}

export interface Guest {
  id: number;
  name: string;
  email: string;
  messageSent: boolean;
  willAttend: WillAttend;
  link: string;
}

export type WillAttend = 'yes' | 'maybe';
