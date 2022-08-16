import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { EmployeeServiceService } from '../employee/employee-service.service';
import { Employee } from '../models/employee';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  EmployeeList?: Observable<Employee[]>;
  EmployeeList1?: Observable<Employee[]>;
  employeeForm: any;
  massage="";

  // Will see these properties later
  prodCategory = "";
  productId = 0;
  
  constructor(private formBuilder:FormBuilder,
    private employeeService:EmployeeServiceService,private router:Router,
    private jwtHelper: JwtHelperService, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.employeeForm = this.formBuilder.group({
      EmployeeName: ['',[Validators.required]],
      Designation: ['',[Validators.required]],
      ContactNo: ['',[Validators.required]],
      EmailID: ['',[Validators.required]],
      SkillSets: ['',[Validators.required]],
    });
    this.getEmployeeList();
  }

  getEmployeeList()
  {
    this.EmployeeList1 = this.employeeService.getEmployeeList();
    this.EmployeeList = this.EmployeeList1;
  }

  // Post
  AddEmployee(employee: Employee){
    console.log("Add Employee Implementation");
  }


  // Employee to Edit
  EmployeeDetailsToEdit(id: string){
    console.log("Edit Employee Details Implementation");

  }
  // Get Details
  GetEmployeeDetails(id: string){
    console.log("Get Employee Details Implementation");
  }

  // Put
  UpdateEmployee(employee: Employee){
    console.log("Update Employee Implementation");

  }
  // Delete
  DeleteEmployee(id:number){
    console.log("Delete Employee Implementation");
  }

  Clear(employee: Employee){
    this.employeeForm.reset();
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/"]);
  }

  isUserAuthenticated(){
    const token = localStorage.getItem("jwt");
    if(token && !this.jwtHelper.isTokenExpired(token)){
      return true;
    }
    else{
      return false;
    }
  }
}