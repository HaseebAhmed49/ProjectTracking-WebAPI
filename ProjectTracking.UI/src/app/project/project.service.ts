import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project } from '../models/project';
import configUrl from '../../assets/Config/config.json';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  url = configUrl.apiServer.url + '/api/projects/';
  constructor(private http:HttpClient) { }

  getProjectList(token:string):Observable<Project[]> {
    const httpHeaders = { 
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })};
      return this.http.get<Project[]>(this.url + 'get-all-projects',httpHeaders);

  }
}
