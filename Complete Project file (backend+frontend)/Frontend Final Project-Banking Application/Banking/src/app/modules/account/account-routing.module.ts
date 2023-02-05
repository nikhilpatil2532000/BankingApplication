import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddAccountComponent } from './components/add-account/add-account.component';
import { DeleteAccountComponent } from './components/delete-account/delete-account.component';
import { EditAccountComponent } from './components/edit-account/edit-account.component';
import { GetAccountComponent } from './components/get-account/get-account.component';
import { AccountListComponent } from './components/account-list/account-list.component';

const routes: Routes = [
  { path: 'add', component: AddAccountComponent },
  { path: 'delete/:id', component: DeleteAccountComponent },
  { path: 'edit/:id', component: EditAccountComponent },
  { path: 'get', component: GetAccountComponent },
  { path: 'list', component: AccountListComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
