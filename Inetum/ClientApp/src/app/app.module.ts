import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { RouterModule } from '@angular/router'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { HttpClientModule } from '@angular/common/http'

import {
  TeamsListComponent,
  EventThumbnailComponent,
  CreateTeamComponent,
  TeamThumbnailComponent,
  TeamDetailsComponent,
  PlayerListComponent,
  CreatePlayerComponent,
  TeamListResolver,
  HomeComponent,
  TeamResolver,
  TeamService
} from './teams/index'
import { TeamsAppComponent } from './teams-app.component'
import { NavBarComponent } from './nav/nav-bar.component'
import { JQ_TOKEN, TOASTR_TOKEN, Toastr, CollapsibleWellComponent, SimpleModalComponent, ModalTriggerDirective } from './common/index'
import { appRoutes } from './routes'
import { Error404Component } from './errors/404.component'
import { AuthService } from './user/auth.service'


let toastr:Toastr = window['toastr'];
let jQuery = window['$'];

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule
  ],
  declarations: [
    TeamsAppComponent,
    HomeComponent,
    TeamsListComponent,
    TeamThumbnailComponent,
    TeamDetailsComponent,
    PlayerListComponent,
    CreatePlayerComponent,
    NavBarComponent,
    CreateTeamComponent,
    
    Error404Component,
    CollapsibleWellComponent,
    ModalTriggerDirective,
    SimpleModalComponent,
    ],
  providers: [
    TeamService, 
    { provide: TOASTR_TOKEN, useValue: toastr}, 
    { provide: JQ_TOKEN, useValue: jQuery}, 
    TeamResolver,
    TeamListResolver,
    AuthService,
    { 
      provide: 'canDeactivateCreateEvent', 
      useValue: checkDirtyState 
    }
  ],
  bootstrap: [TeamsAppComponent]
})
export class AppModule {}

export function checkDirtyState(component:CreateTeamComponent) {
  if (component.isDirty)
    return window.confirm('You have not saved this event, do you really want to cancel?')
  return true
}