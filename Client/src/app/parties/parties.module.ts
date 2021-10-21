import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PartiesComponent } from './parties.component';
import { RouterModule, Routes } from '@angular/router';
import { NewEditPartyComponent } from './new-edit-party/new-edit-party.component';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { PartyComponent } from './party/party.component';

const routes: Routes = [
  { path: '', component: PartiesComponent },
  { path: 'new', component: NewEditPartyComponent },
  { path: ':id/edit', component: NewEditPartyComponent },
  { path: ':id', component: PartyComponent },
];

@NgModule({
  declarations: [PartiesComponent, NewEditPartyComponent, PartyComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatInputModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatIconModule,
    MatButtonModule,
    MatSnackBarModule,
    MatTableModule,
  ],
})
export class PartiesModule {}
