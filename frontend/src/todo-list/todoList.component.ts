import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { CdkDrag, CdkDragDrop, CdkDropList, moveItemInArray } from '@angular/cdk/drag-drop';
import { TodoItem } from '../model/todoItem';

@Component({
    selector: 'todo-list',
    templateUrl: './todoList.component.html',
    styleUrls: ['./todoList.component.css'],
    imports: [CommonModule, CdkDrag, CdkDropList]
})
export class TodoListComponent {
    public items: TodoItem[] = [];

    constructor(private http: HttpClient) { }

    ngOnInit() {
        this.getItemsFromDB();
    }

    public getItemsFromDB() {
        this.http.get<TodoItem[]>("http://localhost:8080/TodoList").subscribe(
            (data: TodoItem[]) => {
                this.items = data.map(item => TodoItem.fromJSON(item));
            },
            (error) => {
                console.error('Error fetching items from DB', error);
            }
        );
    }

    public createNewTodoItem() {
        this.http.post<ResponseType>("http://localhost:8080/TodoList", "", {}).subscribe(
            (response) => {
                this.getItemsFromDB();
            },
            (error) => {
                console.error('POST request failed', error);
            }
        );
    }

    public deleteItemsFromDB(id: string) {
        this.http.delete<ResponseType>(`http://localhost:8080/TodoList`, { params: { id } }).subscribe(
            (response) => {
                this.getItemsFromDB();
            },
            (error) => {
                console.error('DELETE request failed', error);
            }
        );
    }

    public updateItemInDB(item: TodoItem) {
        this.http.put<ResponseType>("http://localhost:8080/TodoList", TodoItem.toJSON(item), {}).subscribe(
            (response) => {
                this.getItemsFromDB();
            },
            (error) => {
                console.error('PUT request failed', error);
            }
        );
    }

    public getTodoItems(): TodoItem[] {
        return this.items.filter(item => !item.completed).sort((a, b) => a.order! - b.order!);
    }

    public getCompletedItems(): TodoItem[] {
        return this.items.filter(item => item.completed);
    }

    public editItem(item: TodoItem, event: Event): void {
        const newValue = (event.target as HTMLInputElement).value;
        item.title = newValue;
        this.updateItemInDB(item);
    }

    public addItem(): void {
        this.createNewTodoItem();
    }

    public deleteItem(id: string): void {
        this.items = this.items.filter(item => item.id !== id);
        this.deleteItemsFromDB(id);
    }

    public drop(event: CdkDragDrop<TodoItem[]>) {
        this.swapItems(event.previousIndex, event.currentIndex);
        // const todos = this.getTodoItems();
        // moveItemInArray(todos, event.previousIndex, event.currentIndex);
        // for (let i = 0; i < todos.length; i++) {
        //     todos[i].order = i;
        // }
        // todos.forEach(item => this.updateItemInDB(item));
    }

    public swapItems(index1: number, index2: number): void {
        const todos = this.getTodoItems();
        moveItemInArray(todos, index1, index2);
        for (let i = 0; i < todos.length; i++) {
            todos[i].order = i;
        }
        todos.forEach(item => this.updateItemInDB(item));
    }

    public toggleCompletion(item: TodoItem): void {
        item.completed = !item.completed;
        if (!item.completed) item.order = Math.max(...this.getTodoItems().map(i => i.order!)) + 1;
        else item.order = -1;
        this.updateItemInDB(item);
    }
}
