import { Base } from './base';
import { PurchaseOrderStatus } from './purchase-order-status.enum';

export class PurchaseOrderItem extends Base {
    id: number;
    name: string;
    description: string;
    justification: string;
    location: string;
    quantity: number;
    price: number;
    subtotal: number;
    poNumber: number;
    reason:string;
    status: PurchaseOrderStatus;
    timestamp:string;
}