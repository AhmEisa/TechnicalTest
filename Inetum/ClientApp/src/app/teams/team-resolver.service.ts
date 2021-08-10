import { Injectable } from '@angular/core'
import { ActivatedRouteSnapshot, Resolve } from '@angular/router'
import { TeamService } from './shared/team.service'

@Injectable()
export class TeamResolver implements Resolve<any> {
  constructor(private teamService:TeamService) {

  }

  resolve(route: ActivatedRouteSnapshot) {
    return this.teamService.getTeam(route.params['id'])
  }
}