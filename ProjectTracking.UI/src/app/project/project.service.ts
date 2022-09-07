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

  deleteProjectById(id: number,token:any): Observable<number>{
    const httpHeaders ={
      headers: new HttpHeaders({
        'Content-Type':'application/json',
        'Access-Control-Allow-Origin':'*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'DELETE',
        'Authorization': 'Bearer '+ token,
      })};
      return this.http.delete<number>(this.url+'delete-project-by-id/'+id,httpHeaders);
    }

    postProjectData(projectData: Project,token:any): Observable<Project>{
      const httpHeaders = { 
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*',
          'Access-Control-Allow-Headers': 'Content-Type',
          'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
          'Authorization': 'Bearer '+ token,
        })};
        return this.http.post<Project>(this.url + 'add-project',projectData,httpHeaders);
  ``}

  updateProject(id:number,project: Project, token: any): Observable<Project>{
    const httpHeaders ={
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })
    };
    return this.http.put<Project>(this.url + 'update-project-by-id/'+ id, project, httpHeaders);
  }

  getProjectDetailsById(id: any,token:any): Observable<Project>{
    const httpHeaders = { 
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })};
    return this.http.get<Project>(this.url + 'get-project-by-id/' + id,httpHeaders);
  }

}
