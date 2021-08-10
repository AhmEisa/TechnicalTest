import { Routes } from '@angular/router'
import { TeamsListComponent, HomeComponent } from './teams/teams-list.component'
import {  TeamDetailsComponent } from './teams/team-details/team-details.component'
import { CreateTeamComponent } from './teams/create-team.component'
import { Error404Component } from './errors/404.component'
import {  TeamListResolver } from './teams/team-list-resolver.service'
import { TeamResolver } from './teams/team-resolver.service'

export const appRoutes:Routes = [
  { path: 'teams/new', component: CreateTeamComponent, canDeactivate: ['canDeactivateCreateEvent'] },
  { path: 'teams', component: TeamsListComponent, resolve: {teams:TeamListResolver} },
  { path: 'teams/:id', component: TeamDetailsComponent,  resolve: {team: TeamResolver} },
  { path: '404', component: Error404Component },
  { path: '', component: HomeComponent},
  { 
    path: 'user', 
    loadChildren: () => import('./user/user.module').then(m => m.UserModule)
  }
]