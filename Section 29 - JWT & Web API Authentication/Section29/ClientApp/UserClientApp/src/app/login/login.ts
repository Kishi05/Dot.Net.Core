import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserAccountService } from '../user-account-service';
import { Router } from '@angular/router';
import { Loginuser } from './Models/loginuser';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  
  loginForm: FormGroup;
  isLoginFormSubmitted: boolean = false;
  isSuccess = false;
  isFailed = false;

      constructor(private userAccountService: UserAccountService, private router:Router){
      this.loginForm = new FormGroup({
        Email: new FormControl(null,[Validators.required]),
        Password: new FormControl(null,[Validators.required])
      });
    }

    get login_Email():any{
      return this.loginForm.controls["Email"];
    }

    get login_Password():any{
      return this.loginForm.controls["Password"];
    }

    loginSubmitted(){
      this.isLoginFormSubmitted = true;

      this.userAccountService.Login(this.loginForm.value).subscribe({
        next: (response: any) => {
          this.userAccountService.userSet(response.personName);
          localStorage["token"] = response.token
          this.router.navigate(['/main']);
          this.isSuccess = true;
          this.isFailed = false;
      },
      error: (error : any) =>{
        this.isSuccess = false;
        this.isFailed = true;
      }
    });

    }

}
