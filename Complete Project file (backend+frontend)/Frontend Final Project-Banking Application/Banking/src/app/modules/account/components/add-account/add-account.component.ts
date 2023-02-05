import { Component, OnInit } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountDataService } from '../../services/account-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IAddAccount } from '../../interfaces/iadd-account';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.scss'],
})
export class AddAccountComponent implements OnInit {

  addAccountForm!: FormGroup;
  message: string = '';
  messageEnable: boolean = false;
  initBalance = 0;
  addAccountModel!: IAddAccount;

  constructor(private _formBuilder: FormBuilder,
    private _accountDataService: AccountDataService,
    private _route: ActivatedRoute,
    private _router: Router) { }

  ngOnInit(): void {
    this.addAccountForm = this._formBuilder.group({
      customerId: ['', [Validators.required, Validators.min(1000)]],
      type: ['', Validators.required],
      totalBalance: ['', [Validators.required, Validators.min(this.initBalance)]],
      status: ['', Validators.required]
    });

  }

  doAddAccount(): void {

  }

  onClickSubmitButton(): void {
    this.addAccountModel = <IAddAccount>this.addAccountForm.value;
    this._accountDataService.addAccount(this.addAccountModel)
      .subscribe((data: string) => {
        this.message = <string>data;
        this.messageEnable = true;
      });

    setTimeout(() => {
      this._router.navigate(["list/../"], { relativeTo: this._route });
    }, 2000);

  }

  checkCustomerId(): boolean {
    return this.addAccountForm.get('customerId')!.hasError('min');
  }


  checkAccountType(): boolean {
    if (this.addAccountForm.get('type')!.value == 'Saving') {
      this.initBalance = 3000;
      this.addAccountForm.get('totalBalance')!.setValidators(Validators.min(this.initBalance));
    }
    else {
      this.initBalance = 100;
      this.addAccountForm.get('totalBalance')!.setValidators(Validators.min(this.initBalance));
    }
    return this.addAccountForm.get('type')!.hasError('required');
  }

  checkBalance(): boolean {
    return this.addAccountForm.get('totalBalance')!.hasError('min');
  }

  resetForm() {
    this.addAccountForm.reset();
  }
}
