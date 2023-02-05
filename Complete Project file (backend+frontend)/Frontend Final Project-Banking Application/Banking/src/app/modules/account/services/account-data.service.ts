import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IGetAllAccount } from '../interfaces/iget-all-account';
import { IAddAccount } from '../interfaces/iadd-account';
import { IUpdateAccount } from '../interfaces/iupdate-account';

@Injectable({
  providedIn: 'root'
})
export class AccountDataService {
  private _url: string = "http://localhost:5432/api/Account/";
  constructor(private _http: HttpClient) { }

  getAccountList(): Observable<IGetAllAccount[]> {
    return this._http.get<IGetAllAccount[]>(this._url + "GetAllAccounts");
  }

  addAccount(formData: IAddAccount): Observable<string> {
    return this._http.post<string>(this._url + "AddAccount", formData);
  }

  updateAccount(accountNumber: number, formData: IUpdateAccount[]): Observable<string> {
    return this._http.patch<string>(this._url + "UpdateAccountDetails?accountNumber=" + accountNumber, formData);
  }

  deleteAccount(accountNumber: number): Observable<string> {
    return this._http.delete<string>(this._url + "DeleteAccountByAccountNumber?accountNumber=" + accountNumber);
  }
}
