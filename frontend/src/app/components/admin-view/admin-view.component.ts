import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-view',
  templateUrl: './admin-view.component.html',
  styleUrls: ['./admin-view.component.scss']
})
export class AdminViewComponent implements OnInit {

  constructor(private router:Router) { }

  ngOnInit(): void {
  }
  userInfo(): void {
    this.router.navigate(['/admin/userinfo']);
  }
  createUser(): void {
    this.router.navigate(['/admin/createuser']);
  }
  createTask(): void {
    this.router.navigate(['/admin/createtodo']);
  }
}
