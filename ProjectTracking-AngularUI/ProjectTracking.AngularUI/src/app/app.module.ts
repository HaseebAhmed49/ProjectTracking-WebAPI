import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GetProjectTasksComponent } from './components/projecttasks/get-project-tasks/get-project-tasks.component';

@NgModule({
  declarations: [
    AppComponent,
    GetProjectTasksComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
