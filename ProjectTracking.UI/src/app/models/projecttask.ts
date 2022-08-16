import { Employee } from "./employee";
import { UserStory } from "./userstory";

export class ProjectTask{
    projectTaskId?: number;
    assignedTo?: string;
    taskStartDate?: Date;
    taskEndDate?: Date;
    taskCompletion?: string;
    employeeId?: number;
    employee?: Employee;
    userStoryId?: number;
    userStory?: UserStory;
}