import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerRoutingModule } from './customer-routing.module';
import { AddCustomerComponent } from './components/add-customer/add-customer.component';
import { GetCustomerComponent } from './components/get-customer/get-customer.component';
import { EditCustomerComponent } from './components/edit-customer/edit-customer.component';
import { DeleteCustomerComponent } from './components/delete-customer/delete-customer.component';
import { CustomerListComponent } from './components/customer-list/customer-list.component';


@NgModule({
  declarations: [
    AddCustomerComponent,
    GetCustomerComponent,
    EditCustomerComponent,
    DeleteCustomerComponent,
    CustomerListComponent
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule
  ]
})
export class CustomerModule { }
