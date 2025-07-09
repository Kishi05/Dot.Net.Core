import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserAccountService } from '../user-account-service';
import { Router } from '@angular/router';
import { RegisterUser } from './register-user';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  registerForm: FormGroup;
  isRegisterFormSubmitted: boolean = false;
  isSuccess = false;
  isFailed = false;

    constructor(private userAccountService: UserAccountService, private router:Router){
      this.registerForm = new FormGroup({
        PersonName: new FormControl(null,[Validators.required]),
        Email: new FormControl(null,[Validators.required]),
        Phone: new FormControl(null,[Validators.required]),
        Password: new FormControl(null,[Validators.required]),
        ConfirmPassword: new FormControl(null,[Validators.required])
      });
    }

    get register_PersonName():any{
        return this.registerForm.controls["PersonName"];
      }

    get register_Email():any{
        return this.registerForm.controls["Email"];
      }

    get register_Phone():any{
        return this.registerForm.controls["Phone"];
      }

    get register_Password():any{
        return this.registerForm.controls["Password"];
      }

    get register_ConfirmPassword():any{
        return this.registerForm.controls["ConfirmPassword"];
      }

      registerSubmitted(){
        this.isRegisterFormSubmitted = true;

        this.userAccountService.Register(this.registerForm.value).subscribe({
          next: (response: any) => {
            //Strore token to local storage
            localStorage["refreshToken"] = response.refreshTokens
            localStorage["token"] = response.token

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
