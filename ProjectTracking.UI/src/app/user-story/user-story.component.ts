import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { ProjecttaskService } from '../project-task/projecttask.service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ToastrService } from 'ngx-toastr';
import { SharedService } from '../shared-service/shared.service';
import { UserStoryService } from './user-story.service';
import { UserStory } from '../models/userstory';

@Component({
  selector: 'app-user-story',
  templateUrl: './user-story.component.html',
  styleUrls: ['./user-story.component.css']
})
export class UserStoryComponent implements OnInit {
  userStoryList?: Observable<UserStory[]>;
  userStoryList1?: Observable<UserStory[]>;
  userstoryForm: any;
  massage="";
  token:any;

  userStoryID?: string = "";
  projectList:any;

  selectedProject = '';
	onSelected(value:string): void {
		this.selectedProject = value;
    console.log(this.selectedProject);
	}
  
  constructor(private formBuilder:FormBuilder,
    private userStoryService:UserStoryService,
    private router:Router,private sharedService:SharedService,
    private jwtHelper: JwtHelperService, private toastr:ToastrService) { }


  ngOnInit(): void {

    this.token = localStorage.getItem("jwt");

    this.sharedService.getProjectList(this.token).subscribe((data:any)=>{
      this.projectList = data;
    });

    this.userstoryForm = this.formBuilder.group({
      story: ['',[Validators.required]],
      projectID: ['',[Validators.required]],
    });

    this.getUserStoryList(this.token);
  }

  getUserStoryList(token:any){
    this.userStoryList1 = this.userStoryService.getUserStoryList(token);
    this.userStoryList = this.userStoryList1;
  }

// Post
AddUserStory(userStoryData: UserStory){
  console.log(this.userstoryForm.value);
  const userstory_data = this.userstoryForm.value;
  this.token = localStorage.getItem("jwt");
  this.userStoryService.postUserStoryData(userStoryData,this.token).subscribe(
    () => {
      this.getUserStoryList(this.token);
      this.userstoryForm.reset();
      this.toastr.success('User Story Added Successfully');
    }, _err => {
      this.toastr.warning('Error in Add User Story');
    }
  );
}

// User Story To Edit
UserStoryDetailsToEdit(id: any){
  this.token = localStorage.getItem("jwt");
  this.userStoryService.getUserStoryDetailsById(id,this.token).subscribe(userStoryResult => {
    this.userStoryID = userStoryResult.userStoryID;
    console.log('Edit Method'+this.userStoryID);
    this.userstoryForm.controls['story'].setValue(userStoryResult.story);
    this.userstoryForm.controls['projectID'].setValue(userStoryResult.projectID);
  });
}

// Put
UpdateUserStory(userStory: UserStory){
  this.token = localStorage.getItem("jwt");
  console.log('Update Method '+this.userStoryID);
  const userStory_Master = this.userstoryForm.value;
  this.userStoryService.updateUserStory(Number(this.userStoryID),userStory_Master,this.token).subscribe(
    () => {
      this.toastr.success("User Story Updated Successfully");
      this.userstoryForm.reset();
      this.getUserStoryList(this.token);
    });
}

// Delete
DeleteUserStory(id:any){
  console.log("Delete Employee Implementation");
  this.token = localStorage.getItem("jwt");
  if(confirm('Do you want to delete this User Story?')){
    console.log(id);
    this.userStoryService.deleteUserStoryById(id,this.token).subscribe(() =>{
      console.log(id);
      this.toastr.success('User Story Deleted Successfully');
      this.getUserStoryList(this.token);
    });
  }
}


  Clear(userStory: UserStory){
    this.userstoryForm.reset();
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
