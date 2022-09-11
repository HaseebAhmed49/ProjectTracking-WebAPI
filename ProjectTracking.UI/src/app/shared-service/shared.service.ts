import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProjectTask } from '../models/projecttask';
import configUrl from '../../assets/Config/config.json';


@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http:HttpClient) { }

  getEmployeeListForProjectTask(token:string):Observable<any[]> {
    const httpHeaders = { 
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })};
      return this.http.get<any[]>(configUrl.apiServer.url + '/api/Employee/get-all-employees',httpHeaders);
  }

  getUserStoryListForProjectTask(token:string):Observable<any[]> {
    const httpHeaders = { 
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })};
      return this.http.get<any[]>(configUrl.apiServer.url + '/api/UserStory/get-all-user-stories',httpHeaders);
  }

}
