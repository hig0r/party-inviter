import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'parties' },
  {
    path: 'parties',
    loadChildren: () =>
      import('./parties/parties.module').then((x) => x.PartiesModule),
  },
  {
    path: 'i',
    loadChildren: () =>
      import('./invite/invite.module').then((x) => x.InviteModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
