import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users/users.service';

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

  constructor(private userService: UsersService) { }

  ngOnInit(): void {
  }

  signInUser(){
    console.log(this.signInRequest);
    this.userService.signInUser(this.signInRequest)
    .subscribe({
      next: (user) => {
        console.log(user);
      }
    });
  }
}
