import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ProjectService } from '../project/project.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Project } from '../models/project';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent implements OnInit {
  projectList?: Observable<Project[]>;
  projectList1?: Observable<Project[]>;
  projectForm: any;
  massage="";
  token:any;

  projectID?: string = "";

  constructor(private formBuilder:FormBuilder,
    private projectService:ProjectService,private router:Router,
    private jwtHelper: JwtHelperService, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.projectForm = this.formBuilder.group({
      ProjectName: ['',[Validators.required]],
      startDate: ['',[Validators.required]],
      endDate: ['',[Validators.required]],
      Client: ['',[Validators.required]],
    });
    this.token = localStorage.getItem("jwt");
    this.getProjectList(this.token);
  }

  getProjectList(token:any){
    this.projectList1 = this.projectService.getProjectList(token);
    this.projectList = this.projectList1;
  }

 // Post
 AddProject(project: Project){
  // const employee_data = this.employeeForm.value;
  // this.token = localStorage.getItem("jwt");
  // this.employeeService.postEmployeeData(employee_data,this.token).subscribe(
  //   () => {
  //     this.getEmployeeList(this.token);
  //     this.employeeForm.reset();
  //     this.toastr.success('Employee Added Successfully');
  //   }
  // );
}


// Project to Edit
ProjectDetailsToEdit(id: any){
  // this.token = localStorage.getItem("jwt");
  // this.employeeService.getEmployeeDetailsById(id,this.token).subscribe(employeeResult => {
  //   this.employeeId = employeeResult.employeeID;
  //   console.log(this.employeeId);
  //   this.employeeForm.controls['EmployeeName'].setValue(employeeResult.employeeName);
  //   this.employeeForm.controls['Designation'].setValue(employeeResult.designation);
  //   this.employeeForm.controls['ContactNo'].setValue(employeeResult.contactNo);
  //   this.employeeForm.controls['EmailID'].setValue(employeeResult.emailID);
  //   this.employeeForm.controls['SkillSets'].setValue(employeeResult.skillSets);
  // });
}

// Put
UpdateProject(project: Project){
  // this.token = localStorage.getItem("jwt");
  // // employee.employeeID = this.employeeId;
  // // console.log(employee.employeeID);
  // const project_Master = this.projectForm.value;
  // this.employeeService.updateEmployee(Number(this.employeeId),employee_Master,this.token).subscribe(
  //   () => {
  //     this.toastr.success("Employee Data Updated Successfully");
  //     this.employeeForm.reset();
  //     this.getEmployeeList(this.token);
  //   });
}

// Delete
DeleteProject(id:any){
  console.log("Delete Employee Implementation");
  // this.token = localStorage.getItem("jwt");
  // if(confirm('Do you want to delete this Employee?')){
  //   console.log(id);
  //   this.employeeService.deleteEmployeeById(id,this.token).subscribe(() =>{
  //     console.log(id);
  //     this.toastr.success('Employee Deleted Successfully');
  //     this.getEmployeeList(this.token);
  //   });
  // }
}


  Clear(project: Project){
    this.projectForm.reset();
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
