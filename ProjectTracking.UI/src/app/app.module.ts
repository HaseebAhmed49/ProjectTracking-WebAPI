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
import { ProjectComponent } from './project/project.component';
import { SignupComponent } from './signup/signup.component';
import { ProjectTaskComponent } from './project-task/project-task.component';
import { UserStoryComponent } from './user-story/user-story.component';

const routes:Routes = [
  {path:'login',component:LoginComponent},
  {path:'',component:LoginComponent},
  {path:'employee',component:EmployeeComponent},
  {path:'project',component:ProjectComponent},
  {path:'signup',component:SignupComponent},
  {path:'projecttask',component:ProjectTaskComponent},
  {path:'userstory',component:UserStoryComponent},
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
    EmployeeComponent,
    ProjectComponent,
    SignupComponent,
    ProjectTaskComponent,
    UserStoryComponent
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
