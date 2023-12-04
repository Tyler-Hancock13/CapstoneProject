import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { GlobalService } from '../services/global.service';
import { Role } from '../models/role.enum';

@Injectable({
  providedIn: 'root',
})
export class UpdateDepartmentGuard implements CanActivate {
  constructor(private globals: GlobalService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    let { user } = this.globals;
    return user.role == Role.HRSupervisor ||
      user.role == Role.HREmployee ||
      user.role == Role.Supervisor
      ? true
      : false;
  }
}
