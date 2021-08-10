
export interface ITeam {
  teamId: string
  name: string
  foundationDate: Date
  country: string
  coachName: string
  logoUrl: string
  players: IPlayer[]
}
export interface IPlayer{
  playerId:string
  name:string
  dateOfBirth:Date
  nationality:string
  imageUrl:string
  teamId:string
}

