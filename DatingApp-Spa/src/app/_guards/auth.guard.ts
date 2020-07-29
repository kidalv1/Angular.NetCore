import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { ifStmt } from '@angular/compiler/src/output/output_ast';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService,
    private router: Router,
    private alertift: AlertifyService) 
    {}
  canActivate(): boolean {
    if(this.authService.loggedIn()){
      return true;
    }
    this.alertift.error("You shall not pass!!!")
    this.router.navigate(["/home"]);
    return false;
    
  }

}
