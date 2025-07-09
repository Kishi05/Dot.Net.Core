import { HttpClient, HttpHeaders } from '@angular/common/http';
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

    public getApiUsers(): Observable<any> {
      const jwt = localStorage.getItem('token'); 
      let headers = new HttpHeaders();
      if(jwt){
        headers = headers.append("Authorization",`Bearer ${localStorage['token']}`);
      }
      return this.httpClient.get(`${API_BASE_URL}/UserList`,{headers : headers});
  }

  public postGenerateNewToken(): Observable<any>{

    const token = localStorage.getItem('token'); 
    const refreshToken = localStorage.getItem('refreshToken'); 

    return this.httpClient.post(`${API_BASE_URL}/generate-new-jwt`,{token : token, refreshToken: refreshToken});

  }

}
