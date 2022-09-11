import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProjectTask } from '../models/projecttask';
import configUrl from '../../assets/Config/config.json';
import { UserStory } from '../models/userstory';

@Injectable({
  providedIn: 'root'
})

export class UserStoryService {
  url = configUrl.apiServer.url + '/api/UserStory/';
  constructor(private http:HttpClient) { }

  getUserStoryList(token:string):Observable<UserStory[]> {
    const httpHeaders = { 
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })};
      return this.http.get<UserStory[]>(this.url + 'get-all-user-stories',httpHeaders);
  }

  deleteUserStoryById(id: number,token:any): Observable<number>{
    const httpHeaders ={
      headers: new HttpHeaders({
        'Content-Type':'application/json',
        'Access-Control-Allow-Origin':'*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'DELETE',
        'Authorization': 'Bearer '+ token,
      })};
      return this.http.delete<number>(this.url+'delete-user-story-by-id/'+id,httpHeaders);
    }

    postUserStoryData(userStoryData: UserStory,token:any): Observable<UserStory>{
      console.log(userStoryData + 'Service Method');
      const httpHeaders = { 
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*',
          'Access-Control-Allow-Headers': 'Content-Type',
          'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
          'Authorization': 'Bearer '+ token,
        })};
        return this.http.post<UserStory>(this.url + 'add-user-story',userStoryData,httpHeaders);
  }

  updateUserStory(id:number,userStory: UserStory, token: any): Observable<UserStory>{
    const httpHeaders ={
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })
    };
    return this.http.put<UserStory>(this.url + 'update-user-story-by-id/'+ id, userStory, httpHeaders);
  }

  getUserStoryDetailsById(id: any,token:any): Observable<UserStory>{
    const httpHeaders = { 
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })};
    return this.http.get<UserStory>(this.url + 'get-all-user-story-by-id/' + id,httpHeaders);
  }
}