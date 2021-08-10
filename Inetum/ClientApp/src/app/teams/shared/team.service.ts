import { HttpClient, HttpHeaders } from '@angular/common/http'
import {  Injectable } from '@angular/core'
import {  Observable, of } from 'rxjs'
import { catchError } from 'rxjs/operators'
import {  IPlayer, ITeam } from './team.model'

@Injectable()
export class TeamService {

  constructor(private http: HttpClient) {

  }

  getTeams():Observable<ITeam[]> {
    return this.http.get<ITeam[]>('/api/Team/all')
      .pipe(catchError(this.handleError<ITeam[]>('getTeams', [])))
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    }
  }
  
  getTeam(id:string):Observable<ITeam> {
    return this.http.get<ITeam>('/api/Team/teamwithplayers?id=' + id)
      .pipe(catchError(this.handleError<ITeam>('getTeam')))
  }

  saveTeam(team) {
    let options = { headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<ITeam>('/api/team', team, options)
    .pipe(catchError(this.handleError<ITeam>('saveTeam')))
  }
  
  savePlayer(player) {
    let options = { headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.http.post<IPlayer>('/api/player', player, options)
    .pipe(catchError(this.handleError<IPlayer>('savePlayer')))
  }

}
