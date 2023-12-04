import { Component, OnInit } from '@angular/core';
import { PurchaseOrder } from '../models/purchase-order';
import { PurchaseOrderService } from '../services/purchase-order.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-purchase-orders',
  templateUrl: './purchase-orders.component.html',
  styleUrls: ['./purchase-orders.component.css']
})
export class PurchaseOrdersComponent implements OnInit {
  searchText: string = '';
  startDate: Date;
  endDate: Date;
  purchaseOrders: Array<PurchaseOrder>;
  purchaseOrder: PurchaseOrder;
  loading: boolean;
  id: number = 1;
  message: string;
  employeeId: string;

  constructor(private service: PurchaseOrderService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    
    this.route.params.subscribe(params => {
      this.employeeId = params['employeeId'];
    });
    this.get(this.employeeId);
  }

  get(employeeId: string){
    //this.clear();
    this.loading = true;

    this.service.get(employeeId)
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

  searchById(){
    this.message = "";

    if(this.searchText != ''){
        this.loading = true;

        this.service.searchOrderById(Number(this.searchText), this.employeeId)
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
            this.message = "No results found for that search.";
          }
        });
      } else {
        this.loading = true;

        this.service.searchByDate(this.startDate, this.endDate, this.employeeId)
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
            this.message = `No results found from dates ${this.startDate} to ${this.endDate}`;
          }
        })
      }
      
    this.searchText = "";
  }

}
