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

  getEmployeeList(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.url + 'get-all-employees');
  }
}
