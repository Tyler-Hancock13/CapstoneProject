import { Injectable } from '@angular/core';
import { SharedService, API_URL } from './shared.service';
import { HttpClient } from '@angular/common/http';
import { GlobalService } from './global.service';
import { Observable } from 'rxjs';
import { PurchaseOrder } from '../models/purchase-order';
import { catchError } from "rxjs/operators";
import { PurchaseOrderItem } from '../models/purchase-order-item';
import { Department } from '../models/department';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root'
})
export class PurchaseOrderService extends SharedService {

  constructor(private http: HttpClient, globals: GlobalService) { super(globals) }

  get(employeeId: string) : Observable<PurchaseOrder[]> {
    const API_METHOD = `${API_URL}/purchaseorders/${employeeId}`;
    return this.http
      .get<PurchaseOrder[]>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  getPOByDepartment(departmentId:number) : Observable<PurchaseOrder[]> {
    const API_METHOD = `${API_URL}/purchaseorders/process/${departmentId}`;
    return this.http
      .get<PurchaseOrder[]>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  getItems(id: number) : Observable<PurchaseOrderItem[]> {
    const API_METHOD = `${API_URL}/purchaseorders/${id}/order`;
    return this.http
      .get<PurchaseOrderItem[]>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  searchOrderById(id: number, employeeId:string) : Observable<PurchaseOrder[]> {
    const API_METHOD = `${API_URL}/purchaseorders/${employeeId}/search/${id}`;
    return this.http
      .get<PurchaseOrder[]>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  searchByDate(startDate:Date, endDate:Date, employeeId:string) : Observable<PurchaseOrder[]> {
    const API_METHOD = `${API_URL}/purchaseorders/${employeeId}/search/${startDate}/${endDate}`;
    return this.http
      .get<PurchaseOrder[]>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  deleteItem(id:number) : Observable<boolean> {
    const API_METHOD = `${API_URL}/purchaseorders/order/${id}`;
    return this.http
      .delete<boolean>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  createItem(item:PurchaseOrderItem) : Observable<number> {
    const API_METHOD = `${API_URL}/purchaseorders/${item.poNumber}/order/create`;
    return this.http
      .post<number>(API_METHOD, item, this.httpOptions())
      .pipe(catchError(this.handleError));
  }

  updateItem(item:PurchaseOrderItem) : Observable<PurchaseOrderItem> {
    const API_METHOD = `${API_URL}/purchaseorders/${item.poNumber}/order/edit/${item.id}`;
    return this.http
      .post<PurchaseOrderItem>(API_METHOD, item, this.httpOptions())
      .pipe(catchError(this.handleError));
  }

  getItem(poId:number, id:number) : Observable<PurchaseOrderItem> {
    const API_METHOD = `${API_URL}/purchaseorders/${poId}/order/edit/${id}`;
    return this.http
      .get<PurchaseOrderItem>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  insertRequestAndItem(item:PurchaseOrderItem, employeeId:string) : Observable<boolean> {
    const API_METHOD = `${API_URL}/purchaseorders/${employeeId}/create`;
    return this.http
      .post<boolean>(API_METHOD, item, this.httpOptions())
      .pipe(catchError(this.handleError));
  }

  updateOrder(id:number) : Observable<PurchaseOrder> {
    const API_METHOD = `${API_URL}/purchaseorders/${id}/order`;
    return this.http
      .post<PurchaseOrder>(API_METHOD, id, this.httpOptions())
      .pipe(catchError(this.handleError));
  }

  getDepartment(id:number): Observable<Department> {
    const API_METHOD = `${API_URL}/purchaseorders/${id}/order/GetDepartment`;
    return this.http
      .get<Department>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  getPurchaseOrderDetails(id:number) : Observable<PurchaseOrder> {
    const API_METHOD = `${API_URL}/purchaseorders/${id}/order/create`;
    return this.http
      .get<PurchaseOrder>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  getSupervisor(id:string) : Observable<Employee> {
    const API_METHOD = `${API_URL}/purchaseorders/${id}/order/GetSupervisor`;
    return this.http
      .get<Employee>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  getPurchaseOrdersByDepartment(id:number) : Observable<PurchaseOrder[]> {
    const API_METHOD = `${API_URL}/purchaseorders/process`;
    return this.http
      .get<PurchaseOrder[]>(API_METHOD)
      .pipe(catchError(this.handleError));
  }

  processItem(item:PurchaseOrderItem) : Observable<PurchaseOrderItem> {
    const API_METHOD = `${API_URL}/purchaseorders/process/${item.id}`;
    return this.http
      .post<PurchaseOrderItem>(API_METHOD, item, this.httpOptions())
      .pipe(catchError(this.handleError));
  }
}