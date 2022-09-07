import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ProjecttaskService } from '../project-task/projecttask.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { Project } from '../models/project';
import { ProjectTask } from '../models/projecttask';


@Component({
  selector: 'app-project-task',
  templateUrl: './project-task.component.html',
  styleUrls: ['./project-task.component.css']
})
export class ProjectTaskComponent implements OnInit {
  projectTaskList?: Observable<ProjectTask[]>;
  projectTaskList1?: Observable<ProjectTask[]>;
  projecttaskForm: any;
  massage="";
  token:any;

  projectTaskID?: string = "";

  constructor(private formBuilder:FormBuilder,
    private projecttaskService:ProjecttaskService,private router:Router,
    private jwtHelper: JwtHelperService, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.projecttaskForm = this.formBuilder.group({
      assignedTo: ['',[Validators.required]],
      taskStartDate: ['',[Validators.required]],
      taskEndDate: ['',[Validators.required]],
      taskCompletion: ['',[Validators.required]],
      employeeID: ['',[Validators.required]],
      userStoryID: ['',[Validators.required]],
    });
    this.token = localStorage.getItem("jwt");
    this.getProjectTaskList(this.token);
  }

  getProjectTaskList(token:any){
    this.projectTaskList1 = this.projecttaskService.getProjectTaskList(token);
    this.projectTaskList = this.projectTaskList1;
  }

 // Post
 AddProjectTask(projectTask: ProjectTask){
  const projecttask_data = this.projecttaskForm.value;
  this.token = localStorage.getItem("jwt");
  this.projecttaskService.postProjectTaskData(projecttask_data,this.token).subscribe(
    () => {
      this.getProjectTaskList(this.token);
      this.projecttaskForm.reset();
      this.toastr.success('Project Task Added Successfully');
    }, err => {
      this.toastr.warning('Error in Add Project Task');
    }
  );
}
// Project to Edit
ProjectTaskDetailsToEdit(id: any){
  this.token = localStorage.getItem("jwt");
  this.projecttaskService.getProjectTaskDetailsById(id,this.token).subscribe(projecttaskResult => {
    this.projectTaskID = projecttaskResult.projectTaskID;
    console.log('Edit Method'+this.projectTaskID);
    this.projecttaskForm.controls['assignedTo'].setValue(projecttaskResult.assignedTo);
    this.projecttaskForm.controls['taskStartDate'].setValue(projecttaskResult.taskStartDate?.toLocaleString());
    this.projecttaskForm.controls['taskEndDate'].setValue(projecttaskResult.taskEndDate?.toLocaleString());
    this.projecttaskForm.controls['taskCompletion'].setValue(projecttaskResult.taskCompletion);
    this.projecttaskForm.controls['employeeID'].setValue(projecttaskResult.employeeID);
    this.projecttaskForm.controls['userStoryID'].setValue(projecttaskResult.userStoryID);
  });
}

// Put
UpdateProjectTask(project: Project){
  this.token = localStorage.getItem("jwt");
  console.log('Update Method '+this.projectTaskID);
  const projecttask_Master = this.projecttaskForm.value;
  this.projecttaskService.updateProjectTask(Number(this.projectTaskID),projecttask_Master,this.token).subscribe(
    () => {
      this.toastr.success("Project Task Updated Successfully");
      this.projecttaskForm.reset();
      this.getProjectTaskList(this.token);
    });
}

// Delete
DeleteProjectTask(id:any){
  console.log("Delete Employee Implementation");
  this.token = localStorage.getItem("jwt");
  if(confirm('Do you want to delete this Employee?')){
    console.log(id);
    this.projecttaskService.deleteProjectTaskById(id,this.token).subscribe(() =>{
      console.log(id);
      this.toastr.success('Project Task Deleted Successfully');
      this.getProjectTaskList(this.token);
    });
  }
}


  Clear(project: Project){
    this.projecttaskForm.reset();
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
  }}
