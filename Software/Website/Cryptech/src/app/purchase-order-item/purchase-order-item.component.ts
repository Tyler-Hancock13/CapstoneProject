import { Component, OnInit } from '@angular/core';
import { PurchaseOrderItem } from '../models/purchase-order-item';
import { PurchaseOrderService } from '../services/purchase-order.service';
import { NgForm } from '@angular/forms';
import { ItemStatus } from '../models/item-status.enum';
import { ActivatedRoute, Router } from '@angular/router';
import { PurchaseOrder } from '../models/purchase-order';
import { Department } from '../models/department';
import { Employee } from '../models/employee';
import { EmployeeService } from '../services/employee.service';
import { DepartmentService } from '../services/department.service';1

@Component({
  selector: 'app-purchase-order-item',
  templateUrl: './purchase-order-item.component.html',
  styleUrls: ['./purchase-order-item.component.css']
})
export class PurchaseOrderItemComponent implements OnInit {

  purchaseOrderItems: Array<PurchaseOrderItem>;
  item:PurchaseOrderItem = new PurchaseOrderItem();
  request:PurchaseOrder = new PurchaseOrder();
  department: Department = new Department();
  supervisor: Employee = new Employee();
  employee: Employee = new Employee();
  currentDate: Date = new Date();
  
  orderStatus:string;
  subtitle:string;
  message: string;
  isValidFormSubmitted: boolean;
  loading:boolean;
  title:string;
  poNumber:number;
  itemStatus:ItemStatus;
  employeeId:string;


  constructor(public service:PurchaseOrderService, private employeeService:EmployeeService, private departmentService: DepartmentService, private route:ActivatedRoute, private router:Router) {
    this.isValidFormSubmitted = false;
   }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.request.poNumber = parseInt(params['poId']);
      this.poNumber = parseInt(params['poId']);
      this.item.id = parseInt(params['id']);
      this.employeeId = params['employeeId'];
    });

    this.employeeService.get(this.employeeId).subscribe((s) => {
      this.employee = s;

      this.service.getSupervisor(this.employee.supervisorID).subscribe( supervisor => {
        this.supervisor = supervisor;

        if(this.supervisor == null){
          this.supervisor = new Employee();
          this.supervisor.firstName = "CEO";
          this.supervisor.lastName = "";
        }
      });

      this.service.getDepartment(this.employee.departmentID).subscribe((d) => {
        this.department = d;
        this.getDepartmentInfo(this.department.id);
      });
    });
    

    if(this.request.poNumber === undefined || this.request.poNumber == null){
      this.request.poNumber = 0;
    }

    if(isNaN(this.item.id) && isNaN(this.request.poNumber)){
      this.title = "Create New Purchase Order";
      this.subtitle = "";
      this.orderStatus = "";
    }
    else {
      this.subtitle = `Purchase Order #${this.poNumber} - `;
    }

    if(!isNaN(this.request.poNumber) && isNaN(this.item.id)){
      this.title = `Add Item to Purchase Order: ${this.request.poNumber}`;
    }

    if(!isNaN(this.item.id)){
      this.title = "Update Existing Item";
      this.getItem(this.request.poNumber, this.item.id);
    }
    this.getItems(this.poNumber);
    // this.getEmployeeInfo(this.employee.id);
    // this.getDepartmentInfo(this.employee.departmentID);
    this.getRequestDetails();
    
    this.orderStatus = `${this.request.status}`;
  }

  public onFormSubmit(form: NgForm){
    this.isValidFormSubmitted = false;
    if(form.invalid){
      return;
    }

    if(this.request.status != 1){
      
    }

    this.loading = true;
    this.item = form.value;
    this.message = '';
    
    if(isNaN(this.item.id) && isNaN(this.request.poNumber)){
      this.createRequest(form);
    }
    else if(this.item.id === undefined) {
      this.createItem(form);
    } else {
      this.updateItem(form);
    }
  }


  getItem(poId:number, id:number){
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

    this.title = `Edit Item #${id}`
  }

  private createItem(form:NgForm) {
    this.item.status = 1;
    this.item.poNumber = this.poNumber;
    this.message = "";

    this.service.createItem(this.item).subscribe(
      returnId => {
        if(returnId != 0){
          this.message = `Item added to purchase order. ID: ${returnId}`;
          this.item = new PurchaseOrderItem();
          this.isValidFormSubmitted = true;
          this.updateOrder();
          form.resetForm();
          this.getItems(this.poNumber);
          // setTimeout(() => {
          //   window.location.reload()
          // }, 3000);
        } else {
          this.isValidFormSubmitted = false;
          this.message = 'An error occurred while adding the item.';
        }
      }, err => {
        console.log(err);
      }
    ).add(() => {
      this.loading = false;
    })
  }

  private updateItem(form: NgForm){
    this.message = "";

    if(this.item.status != 1){
      this.message = "Cannot edit items that have been processed.";
      return;
    }
    this.route.params.subscribe(params => {
      this.item.poNumber = parseInt(params['poId']);
    });
    this.item.status = 1;
    this.service.updateItem(this.item).subscribe(
      (purchaseOrderItem) => {
        
        this.item = purchaseOrderItem;

        if(purchaseOrderItem.errors.length == 0){
          this.isValidFormSubmitted = true;
          this.updateOrder();
          this.getItems(this.request.poNumber);
          this.message = `Item updated. ID: ${this.item.id}`;
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
      this.getItems(this.request.poNumber);
  }

  private createRequest(form: NgForm){
    this.request = new PurchaseOrder();
    this.request.poNumber = 0;
    this.request.employeeId = this.employeeId;
    this.request.departmentId = 2;
    this.request.status = 1;
    this.request.subtotal = 0;
    this.request.salesTax = 0;
    this.request.total = 123.00;
    this.request.dateCreated = new Date();
    this.item.status = 1;

    this.service.insertRequestAndItem(this.item, this.employeeId).subscribe(
      result => {
        if(result) {
          this.message = `New Purchase Order created. ID: ${result}`;
          this.isValidFormSubmitted = true;
          this.router.navigate([`/purchaseorders/${this.employee.id}/order/${result}/create`]);
        } else {
          this.isValidFormSubmitted = false;
          this.message = 'Item was a duplicate item. Item has been merged.';
        }
      }, err => {
        this.message = "An error occurred while creating the purchase order. Please Try Again.";
      }).add(() => {

      });
  }

  calculateSubtotal(){
    this.item.subtotal = this.item.quantity * this.item.price;

    if(isNaN(this.item.subtotal)){
      this.item.subtotal = 0;
    }
  }

  getRequestDetails(){
    this.service.getPurchaseOrderDetails(this.poNumber).subscribe(
      po => {
        this.request = po;
      }, err => {

      }
    )
  }

  getEmployeeInfo(id:string){
    this.employeeService.get(id).subscribe( employee => {
      if(employee === undefined) {
        console.log("Employee is null");
        this.employee = null;
      } else {
        this.employee = Object.assign(this.employee, employee);
      }
    }, err => {
      this.message = err;
    })
  }

  private getDepartmentInfo(departmentId:number){
    this.service.getDepartment(departmentId).subscribe( dept => {
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

  private getSupervisor(){
    this.service.getSupervisor(this.request.supervisorId).subscribe( supervisor => {
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

  updateOrder(){
    this.getEmployeeInfo(this.employeeId);
    this.route.params.subscribe(params => {
      this.poNumber = parseInt(params['id']);
    })
    
    this.service.updateOrder(this.request.poNumber).subscribe(po => {
      if(po != undefined){
        this.message = `Purchase order ID: ${po.poNumber} was successfully updated.`;
      }
    })
  }

  getItems(id:number){
    this.loading = true;

    this.service.getItems(id)
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

}
