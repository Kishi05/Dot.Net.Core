import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Register } from './register/register';
import { Login } from './login/login';
import { Home } from './home/home';
import { Search } from './search/search';

const routes: Routes = [
{path:"register",component:Register},
{path:"login",component:Login},
{path:"main",component:Home},
{path:"search",component:Search},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
