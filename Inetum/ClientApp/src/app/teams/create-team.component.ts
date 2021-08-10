import { Component } from '@angular/core'
import { Router } from '@angular/router'
import { TeamService } from './shared/index'

@Component({
  templateUrl: './create-team.component.html',
  styles: [`
    em {float:right; color:#E05C65; padding-left:10px;}
    .error input {background-color:#E3C3C5;}
    .error ::-webkit-input-placeholder { color: #999; }
    .error :-moz-placeholder { color: #999; }
    .error ::-moz-placeholder {color: #999; }
    .error :ms-input-placeholder { color: #999; }
  `]
})
export class CreateTeamComponent {
  newTeam
  isDirty:boolean = true
  constructor(private router: Router, private teamService:TeamService) {

  }

  saveTeam(formValues) {
    this.teamService.saveTeam(formValues).subscribe(() => {
      this.isDirty = false
      this.router.navigate(['/teams'])
    })
  }

  cancel() {
    this.router.navigate(['/teams'])
  }
}