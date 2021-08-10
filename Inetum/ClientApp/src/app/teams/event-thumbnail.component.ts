import { Component, Input } from '@angular/core'
import { ITeam } from './shared/index'

@Component({
  selector: 'event-thumbnail',
  template: `
    <div class="well hoverwell thumbnail">
     </div>
  `,
  styles: [`
    .thumbnail { min-height: 210px; }
    .pad-left { margin-left: 10px; }
    .well div { color: #bbb; }
  `]
})
export class EventThumbnailComponent {
}

@Component({
  selector: 'team-thumbnail',
  template: `
    <div [routerLink]="['/teams', team.teamId]" class="well hoverwell thumbnail">
      <h2>{{team?.name | uppercase}}</h2>
      <div>Foundation Date: {{team?.foundationDate | date:'yyyy-MM-dd'}}</div>
      <div>Country: {{team?.country }}</div>
      <div>Coach Name: {{team?.coachName }}</div>
      <div *ngIf="team?.logoUrl">
        Online URL: {{team?.logoUrl}}
      </div>
     </div>
  `,
  styles: [`
    .thumbnail { min-height: 210px; }
    .pad-left { margin-left: 10px; }
    .well div { color: #bbb; }
  `]
})
export class TeamThumbnailComponent {
  @Input() team:ITeam

  getStartTimeStyle():any {
    if (this.team && this.team.foundationDate.getTime().toString() === '8:00 am')
      return {color: '#003300', 'font-weight': 'bold'}
    return {}
  }
}