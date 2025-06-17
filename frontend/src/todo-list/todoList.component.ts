import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { CdkDrag, CdkDragDrop, CdkDropList, moveItemInArray } from '@angular/cdk/drag-drop';
import { TodoItem } from '../model/todoItem';

const port = "5118"; //5118

@Component({
    selector: 'todo-list',
    templateUrl: './todoList.component.html',
    styleUrls: ['./todoList.component.css'],
    imports: [CommonModule, CdkDrag, CdkDropList]
})
export class TodoListComponent {
    public items: TodoItem[] = [];

    constructor(private http: HttpClient) { }

    /**
     * Angular lifecycle hook that runs after component initialization.
     * Loads the todo items from the backend.
     */
    ngOnInit() {
        this.getItemsFromDB();
    }

    /**
     * Fetches all todo items from the backend API and updates the local items array.
     */
    public getItemsFromDB() {
        this.http.get<TodoItem[]>(`http://localhost:${port}/TodoList`).subscribe(
            (data: TodoItem[]) => {
                this.items = data.map(item => TodoItem.fromJSON(item));
            },
            (error) => {
                console.error('Error fetching items from DB', error);
            }
        );
    }

    /**
     * Sends a POST request to create a new todo item in the backend,
     * then refreshes the local items list.
     */
    public createNewTodoItem() {
        this.http.post<ResponseType>(`http://localhost:${port}/TodoList`, "", {}).subscribe(
            (response) => {
                this.getItemsFromDB();
            },
            (error) => {
                console.error('POST request failed', error);
            }
        );
    }

    /**
     * Sends a DELETE request to remove a todo item by ID from the backend,
     * then refreshes the local items list.
     */
    public deleteItemsFromDB(id: string) {
        this.http.delete<ResponseType>(`http://localhost:${port}/TodoList`, { params: { id } }).subscribe(
            (response) => {
                this.getItemsFromDB();
            },
            (error) => {
                console.error('DELETE request failed', error);
            }
        );
    }

    /**
     * Sends a PUT request to update a todo item in the backend,
     * then refreshes the local items list.
     */
    public updateItemInDB(item: TodoItem) {
        this.http.put<ResponseType>(`http://localhost:${port}/TodoList`, TodoItem.toJSON(item), {}).subscribe(
            (response) => {
                this.getItemsFromDB();
            },
            (error) => {
                console.error('PUT request failed', error);
            }
        );
    }

    /**
     * Returns all incomplete todo items, sorted by their order.
     */
    public getTodoItems(): TodoItem[] {
        return this.items.filter(item => !item.completed).sort((a, b) => a.order! - b.order!);
    }

    /**
     * Returns all completed todo items.
     */
    public getCompletedItems(): TodoItem[] {
        return this.items.filter(item => item.completed);
    }

    /**
     * Updates the title of a todo item and saves the change to the backend.
     */
    public editItem(item: TodoItem, event: Event): void {
        const newValue = (event.target as HTMLInputElement).value;
        item.title = newValue;
        this.updateItemInDB(item);
    }

    /**
     * Creates a new todo item by calling the backend.
     */
    public addItem(): void {
        this.createNewTodoItem();
    }

    /**
     * Removes a todo item from the local list and deletes it from the backend.
     */
    public deleteItem(id: string): void {
        this.items = this.items.filter(item => item.id !== id);
        this.deleteItemsFromDB(id);
    }

    /**
     * Handles drag-and-drop reordering of todo items.
     * Swaps the positions of two items and updates their order in the backend.
     */
    public drop(event: CdkDragDrop<TodoItem[]>) {
        this.swapItems(event.previousIndex, event.currentIndex);
        // const todos = this.getTodoItems();
        // moveItemInArray(todos, event.previousIndex, event.currentIndex);
        // for (let i = 0; i < todos.length; i++) {
        //     todos[i].order = i;
        // }
        // todos.forEach(item => this.updateItemInDB(item));
    }

    /**
     * Swaps the order of two todo items and updates their order in the backend.
     */
    public swapItems(index1: number, index2: number): void {
        const todos = this.getTodoItems();
        moveItemInArray(todos, index1, index2);
        for (let i = 0; i < todos.length; i++) {
            todos[i].order = i;
        }
        todos.forEach(item => this.updateItemInDB(item));
    }

    /**
     * Toggles the completion status of a todo item and updates its order,
     * then saves the change to the backend.
     */
    public toggleCompletion(item: TodoItem): void {
        item.completed = !item.completed;
        if (!item.completed) item.order = Math.max(...this.getTodoItems().map(i => i.order!)) + 1;
        else item.order = -1;
        this.updateItemInDB(item);
    }
}
