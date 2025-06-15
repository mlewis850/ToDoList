import { TodoItem } from './todoItem';

describe('TodoItem', () => {
    it('should parse JSON', () => {
        const json = '{"id":"1","title":"Test Todo","completed":false}';
        const todoItem = TodoItem.fromJSON(JSON.parse(json));
        expect(todoItem.id).toEqual('1');
        expect(todoItem.title).toEqual('Test Todo');
        expect(todoItem.completed).toBeFalse();
    });

    it('should convert to JSON', () => {
        const todoItem = new TodoItem('1', 'Test Todo', false, 1);
        const json = TodoItem.toJSON(todoItem);
        expect(json).toEqual({
            id: '1',
            title: 'Test Todo',
            completed: false,
            order: 1
        });
    });
});
