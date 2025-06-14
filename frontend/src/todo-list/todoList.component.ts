import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { CdkDrag, CdkDragDrop, CdkDropList, moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
    selector: 'todo-list',
    templateUrl: './todoList.component.html',
    styleUrls: ['./todoList.component.css'],
    imports: [CommonModule, CdkDrag, CdkDropList]
})
export class TodoListComponent {
    public items: string[] = [];

    constructor(private http: HttpClient) { }

    ngOnInit() {
        this.getItemsFromDB();
    }

    public getItemsFromDB() {
        this.http.get<string[]>("http://localhost:8080/TodoList").subscribe(
            (data: string[]) => {
                console.log('GET request successful', data);
                this.items = data;
            },
            (error) => {
                console.error('Error fetching items from DB', error);
            }
        );
    }

    public sendItemsToDB() {
        this.http.post<ResponseType>("http://localhost:8080/TodoList", JSON.stringify(this.items), {}).subscribe(
            (response) => {
                console.log('POST request successful', response);
            },
            (error) => {
                console.error('POST request failed', error);
            }
        );
    }

    public getTodoItems(): string[] {
        return this.items;
    }

    public getCompletedItems(): string[] {
        return this.items;
    }

    public editItem(index: number, event: Event): void {
        const newValue = (event.target as HTMLInputElement).value;
        console.log(`Editing item at index ${index} to new value: ${newValue}`);
        this.items[index] = newValue;
    }

    public addItem(newItem: string): void {
        this.items.push(newItem);
    }

    public deleteItem(index: number): void {
        console.log(`Deleting item at index ${index}`);
        this.items.splice(index, 1);
    }

    drop(event: CdkDragDrop<string[]>) {
        console.log('Drop event:', event.previousIndex, event.currentIndex);
        moveItemInArray(this.items, event.previousIndex, event.currentIndex);
        console.log('Items after drop:', this.items);
    }
}
