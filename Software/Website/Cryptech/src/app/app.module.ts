import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { EmployeesComponent } from './employees/employees.component';
import { EmployeeComponent } from './employee/employee.component';
import { EmployeeService } from './services/employee.service';
import { EmployeeDetailsComponent } from './employee-details/employee-details.component';
import { AuthenticationService } from './services/authentication.service';
import { GlobalService } from './services/global.service';
import { LoginComponent } from './login/login.component';
import { AuthenticationGuard } from './guards/authentication.guard';
import { ReactiveFormsModule } from '@angular/forms';
import { PurchaseOrdersComponent } from './purchase-orders/purchase-orders.component';
import { PurchaseOrderItemsComponent } from './purchase-order-items/purchase-order-items.component';
import { PurchaseOrderItemComponent } from './purchase-order-item/purchase-order-item.component';
import { ReviewFormComponent } from './review-form/review-form.component';
import { DepartmentFormComponent } from './department-form/department-form.component';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { PendingReviewsComponent } from './pending-reviews/pending-reviews.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { ReviewComponent } from './review/review.component';
import { ReviewDetailsComponent } from './review-details/review-details.component';
import { ProcessPurchaseOrderComponent } from './process-purchase-order/process-purchase-order.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    EmployeesComponent,
    EmployeeComponent,
    EmployeeDetailsComponent,
    LoginComponent,
    PurchaseOrdersComponent,
    PurchaseOrderItemsComponent,
    PurchaseOrderItemComponent,
    ReviewFormComponent,
    DepartmentFormComponent,
    EmployeeFormComponent,
    PendingReviewsComponent,
    ReviewsComponent,
    ReviewComponent,
    ReviewDetailsComponent,
    ProcessPurchaseOrderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    EmployeeService,
    AuthenticationService,
    GlobalService,
    AuthenticationGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
