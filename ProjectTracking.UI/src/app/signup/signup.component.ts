import { Component } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import configUrl from '../../assets/Config/config.json';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  invalidPassword?: boolean;
  url = configUrl.apiServer.url + '/api/User/';

  constructor(private router:Router, private http:HttpClient, private jwtHelper:JwtHelperService,
    private toastr:ToastrService) { }

    public signup = (form:NgForm) => {
      const credentials = JSON.stringify(form.value);
      this.http.post(this.url + "sign-up",credentials,{
        headers: new HttpHeaders({
          "Content-Type":"application/json",
          'Access-Control-Allow-Origin': '*'
        })
      }).subscribe(response => {
        const token = (<any>response).access_Token;
        console.log("Registered Successfully");
        localStorage.setItem("jwt",token);
        console.log(token);
        this.invalidPassword=false;
        this.router.navigate(["/login"]);        
      }, err =>{
        this.invalidPassword = true;
      });
    }

      isUserAuthenticated(){
        const token = localStorage.getItem("jwt");
        if (token && !this.jwtHelper.isTokenExpired(token)) {
          return true;
        }
        else {
          return false;
        }
      }
  
    }
