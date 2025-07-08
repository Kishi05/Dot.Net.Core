import { Component } from '@angular/core';
import { UserAccountService } from '../user-account-service';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  userName : string | null = null;

  constructor(private userService: UserAccountService){
    this.userName = this.userService.userGet();
  }

}
