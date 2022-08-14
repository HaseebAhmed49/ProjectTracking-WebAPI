import { Project } from "./project";
import { ProjectTask } from "./projecttask";

export class UserStory{
    userStoryId: any;
    story?: string;
    projectId?: number;
    project?: Project;
    projectTasks[]: ProjectTask;
}