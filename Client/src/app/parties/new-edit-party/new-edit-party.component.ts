import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PartiesService } from '../state/parties.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-new-edit-party',
  templateUrl: './new-edit-party.component.html',
  styleUrls: ['./new-edit-party.component.scss'],
})
export class NewEditPartyComponent implements OnInit {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private partiesService: PartiesService,
    private snackbar: MatSnackBar,
    private router: Router
  ) {
    this.form = fb.group({
      name: ['', Validators.required],
      description: '',
      invitationMessage: ['', Validators.required],
      guests: fb.array([]),
    });

    this.newGuest();
  }

  get guests() {
    return this.form.get('guests') as FormArray;
  }

  newGuest() {
    this.guests.push(
      this.fb.group({
        name: ['', Validators.required],
        email: ['', Validators.email],
      })
    );
  }

  ngOnInit(): void {
  }

  removeGuest(i: number) {
    this.guests.removeAt(i);
  }

  submit() {
    this.partiesService.add(this.form.value).subscribe((_) => {
      this.snackbar.open('Festa adicionada com sucesso!', 'UHUUU!');
      this.router.navigateByUrl('parties');
    });
  }
}
