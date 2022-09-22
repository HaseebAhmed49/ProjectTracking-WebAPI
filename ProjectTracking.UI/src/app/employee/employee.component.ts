import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { EmployeeServiceService } from '../employee/employee-service.service';
import { Employee } from '../models/employee';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { EmployeeWithProjectTasks } from '../models/employeeWithProjectTasks';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  EmployeeList?: Observable<Employee[]>;
  EmployeeList1?: Observable<Employee[]>;

  EmployeeListWithProjects?: Observable<EmployeeWithProjectTasks[]>;
  EmployeeListWithProjects1?: Observable<EmployeeWithProjectTasks[]>;

  employeeForm: any;

  massage="";
  token:any;

  employeeId?: string = "";
  
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
    this.token = localStorage.getItem("jwt");
    this.employeeService.getEmployeeDetailsById(id,this.token).subscribe(employeeResult => {
      console.log(employeeResult.employeeID+'Employee Details to Edit');
      this.employeeId = employeeResult.employeeID;
      this.employeeForm.controls['EmployeeName'].setValue(employeeResult.employeeName);
      this.employeeForm.controls['Designation'].setValue(employeeResult.designation);
      this.employeeForm.controls['ContactNo'].setValue(employeeResult.contactNo);
      this.employeeForm.controls['EmailID'].setValue(employeeResult.emailID);
      this.employeeForm.controls['SkillSets'].setValue(employeeResult.skillSets);
    });
  }

  // Put
  UpdateEmployee(employee: Employee){
    this.token = localStorage.getItem("jwt");
    console.log(this.employeeId+' Update Employee');
    const employee_Master = this.employeeForm.value;
      this.employeeService.updateEmployee(Number(this.employeeId),employee_Master,this.token).subscribe(
        () => {
          this.toastr.success("Employee Data Updated Successfully");
          this.employeeForm.reset();
          this.getEmployeeList(this.token);
        });
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

  DetailsEmployee(id:any){
    console.log(' Employee Details Implementation');
    this.token = localStorage.getItem("jwt");
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