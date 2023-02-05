import { Component, OnInit } from '@angular/core';
import { AccountDataService } from '../../services/account-data.service';
import { IGetAllAccount } from '../../interfaces/iget-all-account';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.scss']
})
export class AccountListComponent implements OnInit {

  accountRepo: IGetAllAccount[] = [];
  constructor(private _accountDataService: AccountDataService) {

  }

  ngOnInit(): void {
    this.getAccountList();
  }

  getAccountList(): void {
    this._accountDataService.getAccountList()
      .subscribe((data: IGetAllAccount[]) => {
        this.accountRepo = data;
      });
  }

}
