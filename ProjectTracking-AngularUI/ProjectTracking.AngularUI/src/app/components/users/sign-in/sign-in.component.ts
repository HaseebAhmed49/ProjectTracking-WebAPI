import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Token } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users/users.service';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

 signInRequest: User ={
   Name:'',
   Email: '',
   Password:''
 };

 invalidLogin?: boolean;

  constructor(private http: HttpClient) { }
  ngOnInit(): void {
  }

  // signInUser(){
  //   console.log(this.signInRequest);
  //   this.userService.signInUser(this.signInRequest)
  //   .subscribe({
  //     next: (token) => {
  //       console.log(token);
  //     }
  //   });
  // }
  baseApiUrl: string = environment.baseApiUrl;

  signInUser(){
    console.log(this.signInRequest);
    this.http.post(this.baseApiUrl + '/api/User/authenticate',this.signInRequest,{
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        'Access-Control-Allow-Origin': '*',
      })
    }).subscribe(response => {
      const token = (<any>response).token;
      localStorage.setItem("jwt", token);
      this.invalidLogin = false;
      console.log(token);

    });
  }
}