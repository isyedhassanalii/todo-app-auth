import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { TodoComponent } from './components/todo/todo.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { CreateTodoComponent } from './components/create-todo/create-todo.component';
import { UserInfoComponent } from './components/user-info/user-info.component';
import { AdminViewComponent } from './components/admin-view/admin-view.component';
import { TopbarComponent } from './components/topbar/topbar.component';
import { AuthInterceptor } from './helpers/auth.interceptor';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { 
  AuthGuardService as RoleGuard 
} from './guard/auth-guard';
import { UpdateStatusComponent } from './components/update-status/update-status.component';

export function tokenGetter() {
  return localStorage.getItem('auth-token');
}
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    TodoComponent,
    CreateTodoComponent,
    UserInfoComponent,
    AdminViewComponent,
    TopbarComponent,
    UpdateStatusComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
       tokenGetter: tokenGetter,
      allowedDomains: ["http://localhost:4200/"]
      },
    })
    
  ],
  providers: [{provide:HTTP_INTERCEPTORS,useClass:AuthInterceptor,multi:true},RoleGuard,
    JwtHelperService],
  bootstrap: [AppComponent]
})
export class AppModule { }
