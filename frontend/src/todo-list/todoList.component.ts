import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'todo-list',
    templateUrl: './todoList.component.html',
    styleUrls: ['./todoList.component.css'],
    imports: [CommonModule]
})
export class TodoListComponent {
    public items = ['Don\'t use me'];

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

    public editItem(index: number, event: Event): void {
        const newValue = (event.target as HTMLInputElement).value;
        console.log(`Editing item at index ${index} to new value: ${newValue}`);
        this.items[index] = newValue;
    }

    public addItem(newItem: string): void {
        this.items.push(newItem);
    }
}
