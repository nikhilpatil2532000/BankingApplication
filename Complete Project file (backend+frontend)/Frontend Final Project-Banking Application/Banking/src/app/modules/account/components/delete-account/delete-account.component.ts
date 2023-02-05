import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountDataService } from '../../services/account-data.service';

@Component({
  selector: 'app-delete-account',
  templateUrl: './delete-account.component.html',
  styleUrls: ['./delete-account.component.scss']
})
export class DeleteAccountComponent implements OnInit {

  id: number = -1;
  message: string = '';
  messageEnable: boolean = false;

  constructor(private _route: ActivatedRoute,
    private _accountDataService: AccountDataService,
    private _router: Router) { }

  ngOnInit(): void {

    this._route.params.subscribe((params: any) => {
      this.id = params['id'];
    });

    this._accountDataService.deleteAccount(this.id)
      .subscribe((data: string) => {
        this.message = data;
        this.messageEnable = true;
      });


    setTimeout(() => {
      this._router.navigate(['list/../../'], { relativeTo: this._route })
    }, 2000);

  }
}
