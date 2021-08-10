import { Injectable } from '@angular/core'
import { Resolve } from '@angular/router'
import { TeamService } from './shared/team.service'

@Injectable()
export class TeamListResolver implements Resolve<any> {
  constructor(private teamService:TeamService) {

  }

  resolve() {
    return this.teamService.getTeams()
  }
}