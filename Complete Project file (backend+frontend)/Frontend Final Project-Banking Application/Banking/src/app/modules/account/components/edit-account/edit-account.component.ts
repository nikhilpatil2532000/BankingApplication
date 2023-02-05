import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { AccountDataService } from '../../services/account-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IUpdateAccount } from '../../interfaces/iupdate-account';

@Component({
  selector: 'app-edit-account',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.scss']
})
export class EditAccountComponent implements OnInit {

  public id = -1;
  UpdateAccountForm!: FormGroup;
  message: string = '';
  messageEnable: boolean = false;
  private _updateAccountModel!:IUpdateAccount[];

  constructor(
    private _formBuilder: FormBuilder,
    private _accountService: AccountDataService,
    private _route: ActivatedRoute,
    private _router: Router)
  {
    this._route.params.subscribe((params: any) => {
      this.id = params['id'];
    });

    this.UpdateAccountForm = this._formBuilder.group({
      customerId: new FormControl(),
      type: new FormControl(),
      totalBalance: ['',Validators.required],
      status: ['', Validators.required]
    });

    this.UpdateAccountForm.controls['customerId'].disable();
    this.UpdateAccountForm.controls['type'].disable();


  }

  ngOnInit(): void { }

  doUpdateAccount(): void { }

  onClickSubmitButton(): void {
    const someObject = [
      {
        path: 'status',
        op: 'replace',
        value: this.UpdateAccountForm.get('status')!.value
      },
      {
        path: 'totalBalance',
        op: 'replace',
        value: this.UpdateAccountForm.get('totalBalance')!.value
      }
    ]

    this._updateAccountModel = <IUpdateAccount[]>someObject;

    this._accountService.updateAccount(this.id, this._updateAccountModel)
      .subscribe((data: string) => {
        this.message = data;
        this.messageEnable = true;
      })

    setTimeout(() => {
      this._router.navigate(['list/../../'], { relativeTo: this._route });
    }, 2000);
  }

  resetForm() {
    this.UpdateAccountForm.reset();
  }
}
