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
  token:any;
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
    this.token = localStorage.getItem("jwt");
    this.getEmployeeList(this.token);
  }

  getEmployeeList(token: any)
  {
    this.EmployeeList1 = this.employeeService.getEmployeeList(token);
    this.EmployeeList = this.EmployeeList1;
  }

  // Post
  AddEmployee(employee: Employee){
    const employee_data = this.employeeForm.value;
    this.token = localStorage.getItem("jwt");
    this.employeeService.postEmployeeData(employee_data,this.token).subscribe(
      () => {
        this.getEmployeeList(this.token);
        this.employeeForm.reset();
        this.toastr.success('Employee Added Successfully');
      }
    );
  }


  // Employee to Edit
  EmployeeDetailsToEdit(id: any){
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
  DeleteEmployee(id:any){
    console.log("Delete Employee Implementation");
    this.token = localStorage.getItem("jwt");
    if(confirm('Do you want to delete this Employee?')){
      console.log(id);
      this.employeeService.deleteEmployeeById(id,this.token).subscribe(() =>{
        console.log(id);
        this.toastr.success('Employee Deleted Successfully');
        this.getEmployeeList(this.token);
      });
    }
  }

  Clear(employee: Employee){
    this.employeeForm.reset();
  }

  public logOut = () => {
    localStorage.removeItem("jwt");
    this.router.navigate(["/login"]);
  }

  isUserAuthenticated(){
    const token = localStorage.getItem("jwt");
    if(token && !this.jwtHelper.isTokenExpired(token)){
      return true;
      console.log("Valid Token");
    }
    else{
      return false;
    }
  }
}