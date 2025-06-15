import { TestBed } from '@angular/core/testing';
import { TodoListComponent } from './todoList.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
describe('TodoListComponent', () => {
    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [TodoListComponent, HttpClientTestingModule],
        }).compileComponents();
    });

    it('should create component', () => {
        const fixture = TestBed.createComponent(TodoListComponent);
        const app = fixture.componentInstance;
        expect(app).toBeTruthy();
    });

    it('should swap items on drag and drop', () => {
        const fixture = TestBed.createComponent(TodoListComponent);
        const component = fixture.componentInstance;
        component.items = [
            { id: '1', title: 'Item 1', completed: false, order: 1 },
            { id: '2', title: 'Item 2', completed: false, order: 2 }
        ];
        component.swapItems(0, 1);
        expect(component.items.find(item => item.id === '1')?.order).toBe(1);
        expect(component.items.find(item => item.id === '2')?.order).toBe(0);
    });

    it('should toggle completion of an item', () => {
        const fixture = TestBed.createComponent(TodoListComponent);
        const component = fixture.componentInstance;
        const item = { id: '1', title: 'Item 1', completed: false, order: 1 };
        component.toggleCompletion(item);
        expect(item.completed).toBeTrue();
        component.toggleCompletion(item);
        expect(item.completed).toBeFalse();
    });

    it('checked items should be in second list', () => {
        const fixture = TestBed.createComponent(TodoListComponent);
        const component = fixture.componentInstance;
        component.items = [
            { id: '1', title: 'Item 1', completed: true, order: 1 },
            { id: '2', title: 'Item 2', completed: false, order: 0 }
        ];
        const checkedItems = component.getCompletedItems();
        expect(checkedItems.length).toBe(1);
        expect(checkedItems[0].id).toBe('1');
    });

    it('should delete an item', () => {
        const fixture = TestBed.createComponent(TodoListComponent);
        const component = fixture.componentInstance;
        component.items = [
            { id: '1', title: 'Item 1', completed: true, order: 0 },
            { id: '2', title: 'Item 2', completed: false, order: 1 }
        ];
        component.deleteItem('1');
        expect(component.items.length).toBe(1);
        expect(component.items[0].id).toBe('2');
    });
});
