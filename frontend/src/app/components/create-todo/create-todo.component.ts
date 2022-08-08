import { Component, OnInit } from '@angular/core';
import { TodoService } from 'src/app/services/todo.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-create-todo',
  templateUrl: './create-todo.component.html',
  styleUrls: ['./create-todo.component.scss']
})
export class CreateTodoComponent implements OnInit {
  submitted=false;
  userData:any;
  userChangeValue:any;
  userStatusValue:any;

  task = {
    title: '',
    userId: '',
    status: ''
  };


  constructor(private todo:TodoService,private user:UsersService) { }

  ngOnInit(): void {
    this.retrieveUsers();
  }
  saveTodo(): void {
    const data = {
      title: this.task.title,
      status: this.userStatusValue,
      userId: this.userChangeValue
    };

    this.todo.create(data)
      .subscribe(
        response => {
          this.submitted = true;
        },
        error => {
          console.log(error);
        });
  }

  newTodo(): void {
    this.submitted = false;
    this.task = {
      title: '',
      status: this.userStatusValue,
      userId: this.userChangeValue,
    };
  }
  retrieveUsers(): void {
    this.user.getAll()
      .subscribe(
        data => {
          this.userData = data;
          console.log(data);
        },
        error => {
          console.log(error);
        });
  }
  onUserSelected(value:string){
    this.userChangeValue=value;
    
    console.log("the selected value is " + value);
}
onStatusChange(value:string){
  this.userStatusValue=value;
}
}
