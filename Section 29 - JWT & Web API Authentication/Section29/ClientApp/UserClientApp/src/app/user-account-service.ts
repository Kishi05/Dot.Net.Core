import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const API_BASE_URL: string = "https://localhost:7103/api/Home";

@Injectable({
  providedIn: 'root'
})
export class UserAccountService {

  private loggedInUser: string | null = null;

  constructor(private httpClient: HttpClient) { }

  public userSet(value: string){
    this.loggedInUser = value;
  }

  public userGet(){
    return this.loggedInUser;
  }

  public Register(registerObj: any):Observable<any>{
    return this.httpClient.post<any>(`${API_BASE_URL}`,registerObj);
  };

    public Login(loginObj: any):Observable<any>{
    return this.httpClient.post<any>(`${API_BASE_URL}/Login`,loginObj);
  };

  public LogOut():Observable<any>{
    return this.httpClient.post<any>(`${API_BASE_URL}/Logout`,{});
  };

}
