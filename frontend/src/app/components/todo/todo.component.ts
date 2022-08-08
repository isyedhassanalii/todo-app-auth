import { Component, OnInit } from '@angular/core';
import { TodoService } from 'src/app/services/todo.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})
export class TodoComponent implements OnInit {

  todoData:any;
  stausValue:any;
  userId:any;
  message:any;
  todoId:any;
  constructor(private todo:TodoService,private tokenStorage:TokenStorageService) { }

  ngOnInit(): void {
    this.tokenStorage.getToken();
    const user =this.tokenStorage.getUser();
   
   this.getTodo(user);
  }

  getTodo(id: string): void {
    this.todo.get(id)
      .subscribe(
         data => {
          this.todoData = data.result;
          data.result.map((data=>{
            console.log(data.status)
            if(data.status==0){
              this.stausValue="InProgress";
            }
            if(data.status==1){
              this.stausValue="Active";
            }
            if(data.status==2){
              this.stausValue="Pending";
            }
            if(data.status==3){
              this.stausValue="Done";
            }
          }))
      
        },
      
        error => {
          console.log(error);
        });
  }

}

