import { Component, OnInit } from '@angular/core';
import { TodoService } from 'src/app/services/todo.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss']
})
export class UserInfoComponent implements OnInit {
userData:any;
todoData:any;
stausValue:any;
  constructor(private users:UsersService,private todo:TodoService) { }

  ngOnInit(): void {
    this.retrieveTodos();
  }

    retrieveTodos(): void {
    this.todo.getAll()
      .subscribe(
        data => {
          this.todoData = data.result;
          data.result.map((data=>{
            console.log(data.status)
           
            if(data.status==0){
              this.stausValue="InProgress"
            }
            else if(data.status==2){
              this.stausValue="Pending";
            }
            else if(data.status==3){
              this.stausValue="Done";
            }
            else if(data.status==1){
              this.stausValue="Active";
            }
            
            
          }))
          
        },
        error => {
          console.log(error);

        });
  }
}
