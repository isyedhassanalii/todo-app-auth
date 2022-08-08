import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';
import jwt_decode from 'jwt-decode';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  form: any = {
    email: null,
    password: null
  };
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];

  constructor(private authService: AuthService, private tokenStorage: TokenStorageService,private router:Router,private todo:TodoService) { }

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.roles = this.tokenStorage.getUser().roles;
    }
    if(this.tokenStorage.getToken()){
      const userRole= this.tokenStorage.getRole();
      if(userRole=="Admin"){
        this.router.navigate['admin/adminview'];
      }else{
        this.router.navigate['todo'];
      }
    }
  }

  onSubmit(): void {
    const { email, password } = this.form;

    this.authService.login(email, password).subscribe({
      next: data => {
        this.tokenStorage.saveToken(data.result.token); 
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.tokenStorage.saveUser(data.result.userId);
        const userRole =data.result.role
        this.tokenStorage.SaveRole(userRole);
        console.log(data.result);
        if(userRole=="User"){
          return this.router.navigate(['/todo']);
        }else if(userRole=="Admin"){
          return this.router.navigate(['/admin/adminview']);
        }else{
          this.isLoginFailed = true;
        }
       
      },
      error: err => {
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    });
  }
  getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch(Error) {
      return null;
    }
  }
  reloadPage(): void {
    window.location.reload();
  }
}
