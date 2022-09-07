import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ProjectService } from '../project/project.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Project } from '../models/project';
import moment123 from 'moment';

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
      ClientName: ['',[Validators.required]],
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
  const project_data = this.projectForm.value;
  this.token = localStorage.getItem("jwt");
  this.projectService.postProjectData(project_data,this.token).subscribe(
    () => {
      this.getProjectList(this.token);
      this.projectForm.reset();
      this.toastr.success('Project Added Successfully');
    }, err => {
      this.toastr.warning('Error in Add Project');
    }
  );
}


// Project to Edit
ProjectDetailsToEdit(id: any){
  this.token = localStorage.getItem("jwt");
  this.projectService.getProjectDetailsById(id,this.token).subscribe(projectResult => {
    this.projectID = projectResult.projectID;
    console.log('Edit Method'+this.projectID);
    this.projectForm.controls['ProjectName'].setValue(projectResult.projectName);
    this.projectForm.controls['startDate'].setValue(projectResult.startDate?.toLocaleString());
    this.projectForm.controls['endDate'].setValue(projectResult.endDate?.toLocaleString());
    this.projectForm.controls['ClientName'].setValue(projectResult.clientName);
  });
}

// Put
UpdateProject(project: Project){
  this.token = localStorage.getItem("jwt");
  console.log('Update Method '+this.projectID);
  const project_Master = this.projectForm.value;
  this.projectService.updateProject(Number(this.projectID),project_Master,this.token).subscribe(
    () => {
      this.toastr.success("Project Data Updated Successfully");
      this.projectForm.reset();
      this.getProjectList(this.token);
    });
}

// Delete
DeleteProject(id:any){
  console.log("Delete Employee Implementation");
  this.token = localStorage.getItem("jwt");
  if(confirm('Do you want to delete this Employee?')){
    console.log(id);
    this.projectService.deleteProjectById(id,this.token).subscribe(() =>{
      console.log(id);
      this.toastr.success('Project Deleted Successfully');
      this.getProjectList(this.token);
    });
  }
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
function moment(startDate: string | undefined, arg1: string): any {
  throw new Error('Function not implemented.');
}

