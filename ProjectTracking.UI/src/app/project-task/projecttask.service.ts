import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProjectTask } from '../models/projecttask';
import configUrl from '../../assets/Config/config.json';

@Injectable({
  providedIn: 'root'
})
export class ProjecttaskService {
  url = configUrl.apiServer.url + '/api/ProjectTask/';
  constructor(private http:HttpClient) { }

  getProjectTaskList(token:string):Observable<ProjectTask[]> {
    const httpHeaders = { 
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })};
      return this.http.get<ProjectTask[]>(this.url + 'get-all-project-tasks',httpHeaders);
  }

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


  deleteProjectTaskById(id: number,token:any): Observable<number>{
    const httpHeaders ={
      headers: new HttpHeaders({
        'Content-Type':'application/json',
        'Access-Control-Allow-Origin':'*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'DELETE',
        'Authorization': 'Bearer '+ token,
      })};
      return this.http.delete<number>(this.url+'delete-project-task-by-id/'+id,httpHeaders);
    }

    postProjectTaskData(projectTaskData: ProjectTask,token:any): Observable<ProjectTask>{
      const httpHeaders = { 
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*',
          'Access-Control-Allow-Headers': 'Content-Type',
          'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
          'Authorization': 'Bearer '+ token,
        })};
        return this.http.post<ProjectTask>(this.url + 'add-project-task',projectTaskData,httpHeaders);
  ``}

  updateProjectTask(id:number,projectTask: ProjectTask, token: any): Observable<ProjectTask>{
    const httpHeaders ={
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })
    };
    return this.http.put<ProjectTask>(this.url + 'update-project-task-by-id/'+ id, projectTask, httpHeaders);
  }

  getProjectTaskDetailsById(id: any,token:any): Observable<ProjectTask>{
    const httpHeaders = { 
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })};
    return this.http.get<ProjectTask>(this.url + 'get-project-task-by-id/' + id,httpHeaders);
  }
}
