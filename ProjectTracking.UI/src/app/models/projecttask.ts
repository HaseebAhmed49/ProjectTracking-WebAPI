import { Employee } from "./employee";
import { UserStory } from "./userstory";

export class ProjectTask{
    projectTaskID?: string;
    assignedTo?: string;
    taskStartDate?: Date;
    taskEndDate?: Date;
    taskCompletion?: string;
    employeeID?: number;
    employee?: Employee;
    userStoryID?: number;
    userStory?: UserStory;
}