import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { AccountListComponent } from './components/account-list/account-list.component';
import { AddAccountComponent } from './components/add-account/add-account.component';
import { EditAccountComponent } from './components/edit-account/edit-account.component';
import { GetAccountComponent } from './components/get-account/get-account.component';
import { DeleteAccountComponent } from './components/delete-account/delete-account.component';
import { AccountDataService } from './services/account-data.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AccountListComponent,
    AddAccountComponent,
    EditAccountComponent,
    GetAccountComponent,
    DeleteAccountComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers:[
    AccountDataService ,
  ]
})
export class AccountModule { }
