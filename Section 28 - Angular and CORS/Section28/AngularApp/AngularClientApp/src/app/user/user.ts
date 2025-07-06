import { Component } from '@angular/core';
import { UserService } from '../sevice/user-service';
import { User } from '../models/user';

@Component({
  selector: 'app-user',
  standalone: false,
  templateUrl: './user.html',
  styleUrl: './user.css'
})

export class UserComponent {
  users: User[] = [];

  constructor(private userService: UserService) {
  }

  ngOnInit() {
    this.userService.getApiUsers().subscribe((response: any) => {
      this.users = response;
      console.log(response);
    });
  }

}
