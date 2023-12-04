import { Component, OnInit } from '@angular/core';
import { PurchaseOrderItem } from '../models/purchase-order-item';
import { PurchaseOrder } from '../models/purchase-order';
import { PurchaseOrderService } from '../services/purchase-order.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee';
import { DepartmentService } from '../services/department.service';
import { Department } from '../models/department';

@Component({
  selector: 'app-purchase-order-items',
  templateUrl: './purchase-order-items.component.html',
  styleUrls: ['./purchase-order-items.component.css']
})
export class PurchaseOrderItemsComponent implements OnInit {

  purchaseOrderItems: Array<PurchaseOrderItem>;
  purchaseOrderItem: PurchaseOrderItem;
  purchaseOrder: PurchaseOrder = new PurchaseOrder();
  department: Department = new Department();
  employee: Employee = new Employee();
  supervisor: Employee;
  currentDate: Date;

  itemToEdit: PurchaseOrderItem;
  loading: boolean;
  id: number;
  message: string;
  isValidFormSubmitted: boolean;
  poNumberToUpdate:number;
  employeeId:string;
  

  constructor(private purchaseOrderService: PurchaseOrderService, private employeeService: EmployeeService, private departmentService: DepartmentService, private route:ActivatedRoute) {
      // this.supervisor = new Employee();
      this.department = new Department();
      this.currentDate = new Date();
      this.purchaseOrderItem = new PurchaseOrderItem();
      this.purchaseOrder = new PurchaseOrder();
   }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = parseInt(params['id']);
      this.employeeId = params['employeeId'];
    });

    this.employeeService.get(this.employeeId).subscribe((s) => {
      this.employee = s;
      
      this.purchaseOrderService.getSupervisor(this.employee.supervisorID).subscribe( supervisor => {
        this.supervisor = supervisor;

        if(this.supervisor == null){
          this.supervisor = new Employee();
          this.supervisor.firstName = "CEO";
          this.supervisor.lastName = "";
        }
      });

      this.purchaseOrderService.getDepartment(this.employee.departmentID).subscribe((d) => {
        this.department = d;
        this.getDepartmentInfo(this.department.id);
      });

      this.purchaseOrderService.getPurchaseOrderDetails(this.id).subscribe((po) => {
        this.purchaseOrder = po;
        this.getRequestDetails(this.purchaseOrder.poNumber);
      })
    });

    this.getItems(this.id);
    this.purchaseOrder = this.getRequestDetails(this.id);
    //this.employee = this.getEmployeeInfo(this.employeeId);

    this.getDepartmentInfo(this.purchaseOrder.departmentId);

    if(this.supervisor == null){
      this.supervisor.firstName = "CEO";
    }
    //this.getSupervisor();
  }


  getItems(id:number){
    this.loading = true;

    this.purchaseOrderService.getItems(id)
      .subscribe(
        (purchaseOrderItems) => {
          this.purchaseOrderItems = purchaseOrderItems;
          
        },
        (err) => {
          this.message = err;
        }
      )
      .add(() => {
        this.loading = false;

        if(!this.purchaseOrderItems || this.purchaseOrderItems.length == 0){
          this.message = "No results found.";
        }
      });
  }

  delete(item:PurchaseOrderItem){
    this.message = "";
    this.loading = true;

    if(item.status != 1){
      this.message = "Cannot remove items that have been processed.";
      this.loading = false;
      return;
    }

    this.purchaseOrderService.deleteItem(item.id)
      .subscribe(
        result => {
          if(result){
            this.message = `Successfully removed item. ID: ${item.id}`;0
            //this.getItems(this.id);

          }
          else {
            this.message = "There was a problem removing the item.";
          }
        }
      )

      this.getItems(this.id);
  }

  getEmployeeInfo(id:string) : Employee{
    this.employeeService.get(id).subscribe( employee => {
      if(employee === undefined) {
        console.log("Employee is null");
        this.employee = null;
      } else {
        this.employee = employee;
      }
    }, err => {
      this.message = err;
    })

    return this.employee;
  }

  updateOrder(){
    this.getEmployeeInfo(this.employeeId);
    this.route.params.subscribe(params => {
      this.poNumberToUpdate = parseInt(params['id']);
    })
    
    this.purchaseOrderService.updateOrder(this.poNumberToUpdate).subscribe(po => {
      if(po != undefined){
        this.message = `Purchase order ID: ${po.poNumber} was successfully updated.`;
      }
    })
  }

  private getDepartmentInfo(departmentId:number){
    this.purchaseOrderService.getDepartment(this.purchaseOrder.departmentId).subscribe( dept => {
      if(dept === undefined){
        console.log("Department is null");
        this.department = null;
      } else {
        this.department = dept;
      }
    }, err => {
      console.error(err.error);
    })
  }

  private getRequestDetails(id:number) : PurchaseOrder {
    this.purchaseOrderService.getPurchaseOrderDetails(id).subscribe(
      po => {
        this.purchaseOrder = po;
      }, err => {

      }
    )

    return this.purchaseOrder;
  }

  private getSupervisor(){
    this.purchaseOrderService.getSupervisor(this.purchaseOrder.supervisorId).subscribe( supervisor => {
      if(supervisor === undefined) {
        console.log("Supervisor is null");
        this.supervisor = null;
      } else {
        this.supervisor = supervisor;
      }
    }, err => {
      console.error(err);
    })
  }
}
