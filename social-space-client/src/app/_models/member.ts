import { Photo } from "./photo"

// we get type safety, auto-complete, intellisense
export interface Member {
    id: number
    userName: string
    fullName: string
    age: number
    photoUrl: string
    gender: string
    maritialStatus: string
    occupation: string
    description: string
    interests: string
    lookingFor: string
    city: string
    country: string
    created: Date
    lastActive: Date
    photos: Photo[]
  }
  

  