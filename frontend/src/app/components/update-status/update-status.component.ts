import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TodoService } from 'src/app/services/todo.service';

@Component({
  selector: 'app-update-status',
  templateUrl: './update-status.component.html',
  styleUrls: ['./update-status.component.scss']
})
export class UpdateStatusComponent implements OnInit {
  userStatusValue:any;
 
  constructor(private route: ActivatedRoute,private todo:TodoService) { }
  todoId:Number =this.route.snapshot.params.id;
  ngOnInit(): void {
  

  }
  onStatusChange(value:Number){
    this.userStatusValue=value;
   
  }

  updateTodo(): void {
    const data={
      todoId:Number(this.todoId),
      status:Number(this.userStatusValue)
    }
    this.todo.update(data)
      .subscribe(
        response => {
          console.log(response);
        },
        error => {
          console.log(error);
        });
  }
}
