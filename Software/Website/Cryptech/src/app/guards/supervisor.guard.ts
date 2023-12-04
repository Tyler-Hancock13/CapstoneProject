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
export class SupervisorGuard implements CanActivate {
  constructor(private globals: GlobalService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (
      this.globals.user.role == Role.HRSupervisor ||
      this.globals.user.role == Role.Supervisor
    ) {
      return true;
    }

    return false;
  }
}
