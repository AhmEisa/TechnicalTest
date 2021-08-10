import { Component, OnInit, Output, EventEmitter } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms'
import { IPlayer } from '../shared/index'

@Component({
  selector: 'create-player',
  templateUrl: './create-player.component.html',
  styles: [`
    em {float:right; color:#E05C65; padding-left:10px;}
    .error input, .error select, .error textarea {background-color:#E3C3C5;}
    .error ::-webkit-input-placeholder { color: #999; }
    .error :-moz-placeholder { color: #999; }
    .error ::-moz-placeholder {color: #999; }
    .error :ms-input-placeholder { color: #999; }
  `]
})
export class CreatePlayerComponent implements OnInit {
  @Output() saveNewPlayer = new EventEmitter()
  @Output() cancelAddPlayer = new EventEmitter()

  newPlayerForm: FormGroup
  name: FormControl
  dateOfBirth: FormControl
  nationality: FormControl
  imageUrl: FormControl
  
  ngOnInit() {
    this.name = new FormControl('', Validators.required)
    this.dateOfBirth = new FormControl('', Validators.required)
    this.nationality = new FormControl('', Validators.required)
    this.imageUrl = new FormControl('', Validators.required)
    
    this.newPlayerForm = new FormGroup({
      name: this.name,
      dateOfBirth: this.dateOfBirth,
      nationality: this.nationality,
      imageUrl: this.imageUrl,
     })
  }

  savePlayer(formValues) {
    let player:IPlayer = {
      playerId: undefined,
      name: formValues.name,
      dateOfBirth: formValues.dateOfBirth,
      nationality: formValues.nationality,
      imageUrl: formValues.imageUrl,
      teamId:undefined
    }
    this.saveNewPlayer.emit(player)
  }

  cancel() {
    this.cancelAddPlayer.emit()
  }
}