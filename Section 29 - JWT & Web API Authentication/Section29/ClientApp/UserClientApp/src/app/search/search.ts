import { Component } from '@angular/core';
import { UserAccountService } from '../user-account-service';
import { Users } from './Models/users';

@Component({
  selector: 'app-search',
  standalone: false,
  templateUrl: './search.html',
  styleUrl: './search.css'
})
export class Search {
  users: Users[] = [];

  constructor(private userService: UserAccountService) {
  }

  ngOnInit(){
        this.userService.getApiUsers().subscribe((response: any) => {
      this.users = response;
      console.log(response);
    });
  }

  refreshButton($event: any): void{
    this.userService.postGenerateNewToken().subscribe({
      next: (response: any) => {
        //Strore token to local storage
        localStorage.removeItem("token");
        localStorage.removeItem("refreshToken");
        
        localStorage["refreshToken"] = response.refreshToken
        localStorage["token"] = response.token
      },
      error: (error: any) =>{

      }
    });
  }
}
