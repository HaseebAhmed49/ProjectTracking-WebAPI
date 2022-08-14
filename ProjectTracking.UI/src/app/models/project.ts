import { UserStory } from "./userstory";

export class Project{
    projectId: any;
    projectName?: string;
    startDate?: Date;
    endDate?: Date;
    clientName?: string;
    userStories[]: UserStory;
}