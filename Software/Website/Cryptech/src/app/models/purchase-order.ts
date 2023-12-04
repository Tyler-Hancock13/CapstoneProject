import { Base } from './base';
import { PurchaseOrderStatus } from './purchase-order-status.enum';

export class PurchaseOrder extends Base {
    poNumber: number;
    //items: PurchaseOrderItems[];
    employeeId: string;
    supervisorId: string;
    employeeName:string;
    departmentId: number;
    departmentName:string;
    subtotal: number;
    salesTax: number;
    total: number;
    dateCreated: Date;
    status: PurchaseOrderStatus
}