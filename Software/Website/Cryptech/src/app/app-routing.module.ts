import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EmployeesComponent } from './employees/employees.component';
import { AuthenticationGuard } from './guards/authentication.guard';
import { LoginComponent } from './login/login.component';
import { PurchaseOrdersComponent } from './purchase-orders/purchase-orders.component';
import { PurchaseOrderItemsComponent } from './purchase-order-items/purchase-order-items.component';
import { PurchaseOrderItemComponent } from './purchase-order-item/purchase-order-item.component';
import { ReviewFormComponent } from './review-form/review-form.component';
import { SupervisorGuard } from './guards/supervisor.guard';
import { DepartmentFormComponent } from './department-form/department-form.component';
import { EmployeeFormComponent } from './employee-form/employee-form.component';
import { UpdateDepartmentGuard } from './guards/update-department.guard';
import { PendingReviewsComponent } from './pending-reviews/pending-reviews.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { ProcessPurchaseOrderComponent } from './process-purchase-order/process-purchase-order.component';

const routes: Routes = [
  // Home
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
  },

  //Employees
  {
    path: 'employees',
    component: EmployeesComponent,
    canActivate: [AuthenticationGuard],
  },

  {
    path: 'employees/update/:id',
    component: EmployeeFormComponent,
    canActivate: [AuthenticationGuard],
  },

  //Reviews
  {
    path: 'reviews/create/:supervisorId',
    component: ReviewFormComponent,
    canActivate: [AuthenticationGuard, SupervisorGuard],
  },

  {
    path: 'reviews/create/:supervisorId/:employeeId',
    component: ReviewFormComponent,
    canActivate: [AuthenticationGuard, SupervisorGuard],
  },

  {
    path: 'reviews/pending/:id',
    component: PendingReviewsComponent,
    canActivate: [AuthenticationGuard, SupervisorGuard],
  },

  {
    path: 'reviews/:id',
    component: ReviewsComponent,
    canActivate: [AuthenticationGuard],
  },

  //Departments
  {
    path: 'departments/update',
    component: DepartmentFormComponent,
    canActivate: [AuthenticationGuard, UpdateDepartmentGuard],
  },

  //Purchase Orders
  {
    path: 'purchaseorders/:employeeId',
    component: PurchaseOrdersComponent,
    canActivate: [AuthenticationGuard],
  },

  //Purchase Order Items
  {
    path: 'purchaseorders/:employeeId/order/:id',
    component: PurchaseOrderItemsComponent,
    canActivate: [AuthenticationGuard],
  },

  {
    path: 'purchaseorders/:employeeId/order/:poId/edit/:id',
    component: PurchaseOrderItemComponent,
    canActivate: [AuthenticationGuard],
  },

  {
    path: 'purchaseorders/:employeeId/order/:poId/create',
    component: PurchaseOrderItemComponent,
    canActivate: [AuthenticationGuard],
  },

  {
    path: 'purchaseorders/:employeeId/create',
    component: PurchaseOrderItemComponent,
    canActivate: [AuthenticationGuard],
  },

  {
    path:'purchaseorders/process/:employeeId',
    component: ProcessPurchaseOrderComponent,
    canActivate: [AuthenticationGuard, SupervisorGuard],
  },

  //Login
  {
    path: 'login',
    component: LoginComponent,
  },

  // 404
  { path: '**', redirectTo: '' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
