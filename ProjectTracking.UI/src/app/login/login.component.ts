import { Component } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import configUrl from '../../assets/Config/config.json';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {
  invalidLogin?: boolean;
  url = configUrl.apiServer.url + '/api/User/';

  constructor(private router:Router, private http:HttpClient, private jwtHelper:JwtHelperService,
    private toastr:ToastrService) { }

    public login = (form:NgForm) => {
      const credentials = JSON.stringify(form.value);
      this.http.post(this.url + "login",credentials,{
        headers: new HttpHeaders({
          "Content-Type":"application/json",
          'Access-Control-Allow-Origin': '*'
        })
      }).subscribe(response => {
        const token = (<any>response).access_Token;
        console.log("Entered in Login Subscribe");
        localStorage.setItem("jwt",token);
        console.log(token);
        this.invalidLogin=false;
        this.toastr.success("Logged in Successfully");
        this.router.navigate(["/employee"]);        
      }, err =>{
        this.invalidLogin = true;
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
