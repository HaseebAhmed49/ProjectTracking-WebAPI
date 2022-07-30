import { HttpClient } from '@angular/common/http';
import { Token } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  baseApiUrl: string = environment.baseApiUrl;

  signInUser(signInRequest: User): Observable<any>{
    return this.http.post(this.baseApiUrl + '/api/User/authenticate',signInRequest);
  }
}