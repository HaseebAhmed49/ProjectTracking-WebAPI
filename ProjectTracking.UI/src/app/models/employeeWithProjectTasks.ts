import { ProjectTaskForEmployee } from "./projecttaskForEmployee";

export class EmployeeWithProjectTasks{
    employeeName?: string;
    designation?: string;
    contactNo?: string;
    emailID?: string;
    skillSets?: string;
    ProjectTasks?: Array<ProjectTaskForEmployee>;
}