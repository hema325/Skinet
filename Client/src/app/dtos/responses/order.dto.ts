import { DeliveryMethodDto } from "./delivery-method.dto"
import { OrderItemDto } from "./order-item.dto"

export interface OrderDto {
    id: number
    orderDate: string
    deliveryMethod: DeliveryMethodDto
    items: OrderItemDto[]
    status: string
    subTotal: number
    total: number
}