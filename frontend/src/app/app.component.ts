import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TodoListComponent } from '../todo-list/todoList.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TodoListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ToDoList';
  public items = ['Todo 1', 'Todo 2', 'Todo 3'];
}
