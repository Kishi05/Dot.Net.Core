import { Injectable } from "@angular/core";
import { User } from "../models/user";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})

export class UserService {
  user: User[] = [];
  constructor(private httpClient: HttpClient) {
    this.user = [
      new User('1001', 'Sam','sam@debounceTime.com','UK','2025-07-06','2025-07-06'),
      new User('1002', 'Jack','jack@debounceTime.com','UK','2025-07-06','2025-07-06'),
      new User('1003', 'Samual','samual@debounceTime.com','UK','2025-07-06','2025-07-06'),
      new User('1004', 'John','john@debounceTime.com','UK','2025-07-06','2025-07-06'),
      new User('1005', 'Jack','jack@debounceTime.com','UK','2025-07-06','2025-07-06')
    ];
  }

  public getLocalUsers(): User[] {
    return this.user;
  }

  public getApiUsers(): Observable<any> {
    let headers = new HttpHeaders();
    headers = headers.append("Field-Key","Field-Value");
    headers = headers.append("Authorization","Bearer Test");
    return this.httpClient.get("https://localhost:7105/api/v1/UserVersion",{headers : headers});
  }

}
