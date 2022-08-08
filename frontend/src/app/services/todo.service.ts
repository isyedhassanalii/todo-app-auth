import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const baseUrl ='https://localhost:44394/api/Todo';
 

@Injectable({
  providedIn: 'root'
})

export class TodoService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<any> {
    return this.http.get(baseUrl + '/getTodos');
  }

  get(id): Observable<any> {
    return this.http.get(`${baseUrl}/getTodosByUserId?userId=${id}`);
  }

  create(data): Observable<any> {
    return this.http.post(baseUrl + 'create', data);
  }

  update(data): Observable<any> {
    return this.http.put(`${baseUrl}/updateTodo`,data);
  }

  delete(id): Observable<any> {
    return this.http.delete(`${baseUrl}/${id}`);
  }

  deleteAll(): Observable<any> {
    return this.http.delete(baseUrl);
  }



  
}
