import { Component } from '@angular/core'
import { AuthService } from './user/auth.service';

@Component({
  selector: 'teams-app',
  template: `
    <nav-bar></nav-bar>
    <router-outlet></router-outlet>
  `
})
export class TeamsAppComponent {

  constructor(private auth: AuthService) {
    
  }

  ngOnInit() {}
}
