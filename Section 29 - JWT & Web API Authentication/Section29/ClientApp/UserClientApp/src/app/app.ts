import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserAccountService } from './user-account-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: false,
  styleUrl: './app.css'
})
export class App {
  protected title = 'UserClientApp';
  isLoggedIN = false;
  constructor(private userAccountService : UserAccountService, public router: Router) {}

    logOut(event: Event){
      event.preventDefault(); 
            this.userAccountService.LogOut().subscribe({
              next: (response : any) => {
                localStorage.removeItem("token");
                localStorage.removeItem("refreshToken");
                this.router.navigate(['/login']);
            },
            error: (error : any) =>{
              console.log(error);
            }
          });
  }

  Search(event: Event){
    event.preventDefault(); 
    this.router.navigate(['/search']);
  }

}
