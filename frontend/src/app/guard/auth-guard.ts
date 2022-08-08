import { Injectable } from '@angular/core';
import { 
  Router,
  CanActivate,
  ActivatedRouteSnapshot
} from '@angular/router';
import { AuthService } from '../services/auth.service';
import decode from 'jwt-decode';
import { TokenStorageService } from '../services/token-storage.service'

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {

  constructor(public auth: AuthService, public router: Router,private tokenStorage:TokenStorageService) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data.expectedRole;
    const userRole=this.tokenStorage.getRole()
    if (
      !this.auth.isAuthenticated() || 
      userRole !== expectedRole
    ) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
