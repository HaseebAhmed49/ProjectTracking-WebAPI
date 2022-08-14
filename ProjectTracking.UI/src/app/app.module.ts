import { HttpClientModule } from '@angular/common/http';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './login/login.component';
import { EmployeeComponent } from './employee/employee.component';

import { RouterModule, Routes } from '@angular/router';
import { JwtModule } from "@auth0/angular-jwt";
import { AuthGuard } from './guards/auth-guard.service';
import { ToastrModule } from 'ngx-toastr';

const routes:Routes = [
  {path:'',component:HomepageComponent},
  {path:'employee',component:EmployeeComponent},
  {path:'login',component:LoginComponent},
];


export function tokenGetter()
{
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    LoginComponent,
    EmployeeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes),
    JwtModule.forRoot({
      config:{
        tokenGetter: tokenGetter,
        allowedDomains:["localhost:7153"],
        disallowedRoutes:[]
      }
    }),
    ToastrModule.forRoot()
  ],
  schemas: [ NO_ERRORS_SCHEMA ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
