import { Project } from "./project";
import { ProjectTask } from "./projecttask";

export class UserStory{
    userStoryID?: string;
    story?: string;
    projectID?: string;
    project?: Project;
 //   projectTasks: ProjectTask;
}