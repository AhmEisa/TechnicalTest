import { Component, OnInit } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { ITeam } from './shared/index'

@Component({
  template: `
  <div>
    <h1>Welcome</h1>
    <hr/>
    <div class="row">
      
    </div>
  </div>
  `
})
export class HomeComponent implements OnInit {
  constructor() {
  }
  ngOnInit() {
    
  }
}

@Component({
  template: `
  <div>
    <h1>Inetum Teams</h1>
    <hr/>
    <div class="row">
      <div *ngFor="let team of teams" class="col-md-5">
        <team-thumbnail [team]="team"></team-thumbnail>
      </div>
    </div>
  </div>
  `
})
export class TeamsListComponent implements OnInit {
  teams:ITeam[]

  constructor(private route:ActivatedRoute) {
    
  }

  ngOnInit() {
    this.teams = this.route.snapshot.data['teams']
  }
}