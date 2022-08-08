
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

const TOKEN_KEY = 'auth-token';
const USER_ROLE='auth-role';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class TokenStorageService {
  constructor(private router:Router) { }

  signOut(): void {
    localStorage.clear();
  }

  public saveToken(token: string): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.setItem(TOKEN_KEY,token);
  }

  public getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }
  public SaveRole(user: any): void {
    localStorage.removeItem(USER_ROLE);
    localStorage.setItem(USER_ROLE, JSON.stringify(user));
  }
  public getRole(): any {
    const role = localStorage.getItem(USER_ROLE);
    if (role) {
      return JSON.parse(role);
    }

    return {};
  }
  public saveUser(user: any): void {
    localStorage.removeItem(USER_KEY);
    localStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): any {
    const user = localStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }
  isLoggenIn() {
    const token = this.getToken();
    return !!token;
  }

  loggedOut() {
    localStorage.removeItem(USER_KEY);
    localStorage.removeItem(TOKEN_KEY);
    console.log("logged out");
    this.router.navigate(['/login']);
  
  }
}