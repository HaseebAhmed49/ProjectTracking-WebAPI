import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';
import configUrl from '../../assets/Config/config.json';

@Injectable({
  providedIn: 'root'
})
export class EmployeeServiceService {
  url = configUrl.apiServer.url + '/api/Employee/';
  
  constructor(private http:HttpClient){}

  getEmployeeList(token:string): Observable<Employee[]> {
    const httpHeaders = { 
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
        'Authorization': 'Bearer '+ token,
      })};
    return this.http.get<Employee[]>(this.url + 'get-all-employees',httpHeaders);
  }

  deleteEmployeeById(id: number,token:any): Observable<number>{
    const httpHeaders ={
      headers: new HttpHeaders({
        'Content-Type':'application/json',
        'Access-Control-Allow-Origin':'*',
        'Access-Control-Allow-Headers': 'Content-Type',
        'Access-Control-Allow-Methods': 'DELETE',
        'Authorization': 'Bearer '+ token,
      })};
      return this.http.delete<number>(this.url+'delete-employee-by-id/'+id,httpHeaders);
    }

    postEmployeeData(employeeData: Employee,token:any): Observable<Employee>{
      const httpHeaders = { 
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'Access-Control-Allow-Origin': '*',
          'Access-Control-Allow-Headers': 'Content-Type',
          'Access-Control-Allow-Methods': 'GET,POST,OPTIONS,DELETE,PUT',
          'Authorization': 'Bearer '+ token,
        })};
        return this.http.post<Employee>(this.url + 'add-employee',employeeData,httpHeaders);
  ``    }
  }
