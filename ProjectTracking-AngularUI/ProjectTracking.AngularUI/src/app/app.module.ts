import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GetProjectTasksComponent } from './components/projecttasks/get-project-tasks/get-project-tasks.component';
import { SignInComponent } from './components/users/sign-in/sign-in.component';

@NgModule({
  declarations: [
    AppComponent,
    GetProjectTasksComponent,
    SignInComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
