import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCustomerComponent } from './components/add-customer/add-customer.component';
import { DeleteCustomerComponent } from './components/delete-customer/delete-customer.component';
import { EditCustomerComponent } from './components/edit-customer/edit-customer.component';
import { GetCustomerComponent } from './components/get-customer/get-customer.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';

const routes: Routes = [
  { path:'add',component:AddCustomerComponent},
  { path:'delete',component:DeleteCustomerComponent},
  { path:'edit',component:EditCustomerComponent},
  { path:'get',component:GetCustomerComponent},
  { path:'list',component:CustomerListComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomerRoutingModule { }
