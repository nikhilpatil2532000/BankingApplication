import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { FileNotFoundErrorComponent } from './components/file-not-found-error/file-not-found-error.component';
import { CustomerModule } from './modules/customer/customer.module';
import { AccountModule } from './modules/account/account.module';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'customer', loadChildren: ()=> CustomerModule },
  { path: 'account', loadChildren: ()=> AccountModule },
  { path: '**', component: FileNotFoundErrorComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
