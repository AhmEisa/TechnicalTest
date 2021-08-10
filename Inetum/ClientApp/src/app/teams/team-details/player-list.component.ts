import { Component, Input, OnChanges } from '@angular/core'
import { AuthService } from 'src/app/user/auth.service';
import { IPlayer } from '../shared/index'


@Component({
  selector: 'player-list',
  templateUrl: './player-list.component.html'
})
export class PlayerListComponent implements OnChanges {
  @Input() players:IPlayer[];
  @Input() teamId: string;
  
  constructor(public auth:AuthService) {
    
  }
  ngOnChanges() {}
}
