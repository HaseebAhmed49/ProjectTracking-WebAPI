import { Project } from "./project";
import { ProjectTask } from "./projecttask";

export class UserStory{
    userStoryId?: number;
    story?: string;
    projectId?: number;
    project?: Project;
 //   projectTasks[]: ProjectTask;
}