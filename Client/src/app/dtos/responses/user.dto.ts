import { AddressDto } from "./address.dto"

export interface UserDto {
    id: number
    name: string
    email: string
    address: AddressDto
}