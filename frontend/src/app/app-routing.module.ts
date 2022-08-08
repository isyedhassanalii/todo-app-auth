import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminViewComponent } from './components/admin-view/admin-view.component';
import { CreateTodoComponent } from './components/create-todo/create-todo.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { TodoComponent } from './components/todo/todo.component';
import { UpdateStatusComponent } from './components/update-status/update-status.component';
import { UserInfoComponent } from './components/user-info/user-info.component';
import { 
  AuthGuardService as RoleGuard
} from './guard/auth-guard';

const routes: Routes = [
  {path:'',component:TodoComponent, canActivate: [RoleGuard], 
  data: { 
    expectedRole: 'User'
  }},
  {path:'update/:id',component:UpdateStatusComponent, canActivate: [RoleGuard], 
  data: { 
    expectedRole: 'User'
  }},
  {path:'admin/adminview',component:AdminViewComponent, canActivate: [RoleGuard], 
  data: { 
    expectedRole: 'Admin'
  }  },
  {path:'admin/createuser',component:RegisterComponent,canActivate: [RoleGuard], 
  data: { 
    expectedRole: 'Admin'
  }  },
  {path:'admin/createtodo',component:CreateTodoComponent,canActivate: [RoleGuard], 
  data: { 
    expectedRole: 'Admin'
  }},
  {path:'admin/userinfo',component:UserInfoComponent,canActivate: [RoleGuard], 
  data: { 
    expectedRole: 'Admin'
  } },
  {path:'login',component:LoginComponent},
  {path:'todo',component:TodoComponent,canActivate: [RoleGuard], 
  data: { 
    expectedRole: 'User'
  }},
  { path: '**', component: TodoComponent,canActivate: [RoleGuard], 
  data: { 
    expectedRole: 'User'
  } }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
