import { Component } from '@angular/core'
import { TeamService } from '../shared/team.service'
import { ActivatedRoute, Params } from '@angular/router'
import {  IPlayer, ITeam } from '../shared/index'

@Component({
  templateUrl: './team-details.component.html',
  
  styles: [`
    .container { padding-left:20px; padding-right:20px; }
    .event-image { height: 100px; }
    a {cursor:pointer}
  `]
})
export class TeamDetailsComponent {
  team:ITeam
  addMode:boolean
  filterBy: string = 'all';
  sortBy: string = 'votes';

  constructor(private teamService:TeamService, private route:ActivatedRoute) {

  }
  ngOnInit() {

    this.route.data.forEach((data) => {
      this.team = data['team']
      this.addMode = false;
    })
  }

  addPlayer() {
    this.addMode = true
  }

  saveNewPlayer(player:IPlayer) {
    const nextId = Math.max.apply(null, this.team.players.map(s => s.playerId));
    player.teamId=this.team.teamId;
    this.team.players.push(player);
    this.teamService.savePlayer(player).subscribe();
    this.addMode = false
  }

  cancelAddPlayer() {
    this.addMode = false
  }
}