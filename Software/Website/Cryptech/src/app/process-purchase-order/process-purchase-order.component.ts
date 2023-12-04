import { Component, OnInit } from '@angular/core';
import { PurchaseOrder } from '../models/purchase-order';
import { PurchaseOrderItem } from '../models/purchase-order-item';
import { NgForm } from '@angular/forms';
import { PurchaseOrderService } from '../services/purchase-order.service';
import { ActivatedRoute } from '@angular/router';
import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { Department } from '../models/department';
import { DepartmentService } from '../services/department.service';

@Component({
  selector: 'app-process-purchase-order',
  templateUrl: './process-purchase-order.component.html',
  styleUrls: ['./process-purchase-order.component.css']
})
export class ProcessPurchaseOrderComponent implements OnInit {

  purchaseOrders:Array<PurchaseOrder>;
  purchaseOrder:PurchaseOrder;
  items:Array<PurchaseOrderItem>;
  item:PurchaseOrderItem;
  employee:Employee;
  department:Department;
  currentDate:Date;
  
  loading:boolean;
  isValidFormSubmitted:boolean;
  poMessage:string;
  itemMessage:string;
  message:string;
  employeeId:string;
  details:boolean;
  itemFlag:boolean;
  reasonRequired:boolean;
  reasonMessage:string;

  constructor(private service: PurchaseOrderService, private employeeService:EmployeeService, private departmentService:DepartmentService, private route:ActivatedRoute) {
      this.purchaseOrders = new Array<PurchaseOrder>();
      this.purchaseOrder = new PurchaseOrder();
      this.items = new Array<PurchaseOrderItem>();
      this.item = new PurchaseOrderItem();
      this.department = new Department();
      this.currentDate = new Date();
   }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      this.employeeId = params['employeeId'];
    });

  this.employeeService.get(this.employeeId).subscribe((s) => {
      this.employee = s;

      this.service.getDepartment(this.employee.departmentID).subscribe((d) => {
        this.department = d;
        this.getPOByDepartment(this.department.id);
      });
    });
    // this.employee = this.getEmployeeInfo(this.employeeId);
    // this.getDepartmentInfo(this.employee.departmentID);
    this.details = false;
    this.itemFlag = false;
    this.reasonRequired = false;
  }

  public onFormSubmit(form: NgForm){
    this.isValidFormSubmitted = false;
    if(form.invalid){
      return;
    }

    this.loading = true;
    this.item = form.value;

    if(this.item.status == 3 && this.item.reason == undefined){
      this.reasonMessage = "Reason is required for denied items.";
      return;
    }

    if(this.reasonRequired && this.item.reason == undefined) {
      this.reasonMessage = "Reason is required for modified items.";
    }

    this.updateItem(form);
  }

  get() {
    this.service.get(this.employeeId).subscribe( po => {
      this.purchaseOrders = po;
    })
  }

  getEmployeeInfo(id:string) : Employee{
    this.employeeService.get(id).subscribe( (employee) => {
      if(employee === undefined) {
        console.log("Employee is null");
        this.employee = null;
      } else {
        return employee;
      }
    }, err => {
      console.error(err);
    })

    return this.employee;
  }

  getPOByDepartment(departmentId:number){
    //this.clear();
    this.loading = true;

    this.service.getPOByDepartment(departmentId)
      .subscribe(
        (purchaseOrders) => {
          this.purchaseOrders = purchaseOrders;
        },
        (err) => {
          this.message = err;
        }
      )
      .add(() => {
        this.loading = false;

        if(!this.purchaseOrders || this.purchaseOrders.length == 0){
          this.message = "No results found.";
        }
      });
  }

  private getDepartmentInfo(departmentId:number) : Department{
    this.service.getDepartment(this.purchaseOrder.departmentId).subscribe( dept => {
      if(dept === undefined){
        console.log("Department is null");
        this.department = null;
      } else {
        this.department = dept;
      }
    }, err => {
      console.error(err.error);
    })

    return this.department;
  }

  getItems(poNumber: number, poEmployeeId:string){

    this.loading = true;
    this.poMessage = "";

    if(poEmployeeId == this.employeeId){
      this.poMessage = "Supervisors cannot process their own Purchase Orders.";
      return; 
    }

    this.service.getItems(poNumber)
      .subscribe(
        (purchaseOrderItems) => {
          this.items = purchaseOrderItems;
          this.itemFlag = true;
        },
        (err) => {
          this.message = err;
        }
      )
      .add(() => {
        this.loading = false;

        if(!this.items || this.items.length == 0){
          this.message = "No results found.";
        }
      });

      this.getPODetails(poNumber);
  }

  getPODetails(poId:number){
    this.service.getPurchaseOrderDetails(poId).subscribe( (po) => {
      this.purchaseOrder = po;
    });
  }

  getItemToEdit(poId:number, id:number){
    this.details = true;
    this.message = "";

    this.service.getItem(poId, id).subscribe( (item) => {

      this.item = item;

      if(item === undefined) {
        console.log("Item is null");
        this.message = `Item not found.`;
        this.item = null;
      }
    }, err => {
      this.message = err;
    })
  }

  private updateItem(form: NgForm){
    this.message = "";
    this.item.poNumber = this.purchaseOrder.poNumber;
    if(this.purchaseOrder.employeeId == this.employeeId){
      this.message = "Supervisor cannot process their own Purchase Orders";
      return;
    }
    this.service.processItem(this.item).subscribe(
      (purchaseOrderItem) => {
        
        this.item = purchaseOrderItem;

        if(purchaseOrderItem.errors.length == 0){
          this.message = `Item was processed successfully.`;
          this.isValidFormSubmitted = true;
          this.getPOByDepartment(this.department.id);
          this.getItems(this.item.poNumber, this.employeeId);
        } else {
          this.isValidFormSubmitted = false;
          this.message = "Item was a duplicate. Item has been merged.";
        }
      }, err => {
        console.error(err);
        this.message = err.error;
      });
      form.resetForm();
      // setTimeout(() => {
      //   window.location.reload();
      // }, 3000);
      //this.getItems(this.item.poNumber);
  }

  calculateSubtotal(){
    this.reasonRequired = true;

    this.item.subtotal = this.item.quantity * this.item.price;

    if(isNaN(this.item.subtotal)){
      this.item.subtotal = 0;
    }
  }
}
