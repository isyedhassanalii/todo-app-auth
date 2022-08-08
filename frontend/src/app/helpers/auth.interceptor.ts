// import { HTTP_INTERCEPTORS, HttpEvent } from '@angular/common/http';
// import { Injectable } from '@angular/core';
// import { HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';

// import { TokenStorageService } from '../services/token-storage.service';
// import { Observable } from 'rxjs';

// // const TOKEN_HEADER_KEY = 'Authorization';      
// const TOKEN_HEADER_KEY = 'x-access-token';   

// @Injectable()
// export class AuthInterceptor implements HttpInterceptor {
//   constructor(private token: TokenStorageService) { }

//   intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//     let authReq = req;
//     const token = this.token.getToken();
//     if (token != null) {
      
//       authReq = req.clone({ headers: req.headers.set(TOKEN_HEADER_KEY, 'Bearer ' + token) });

      
//       authReq = req.clone({ headers: req.headers.set(TOKEN_HEADER_KEY, token) });
//     }
//     return next.handle(authReq);
//   }
// }

// export const authInterceptorProviders = [
//   { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
// ];
import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http'
import { TokenStorageService } from '../services/token-storage.service'
@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private injector: Injector,private token:TokenStorageService){}
  intercept(req, next) {
    let authService = this.injector.get(TokenStorageService)
    let tokenizedReq = req.clone(
      {
        headers: req.headers.set('Authorization', 'bearer ' + this.token.getToken())
      }
    )
    return next.handle(tokenizedReq)
  }

}