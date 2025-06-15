export class TodoItem {
    constructor(public id: string, public title: string, public completed: boolean = false, public order?: number) { }

    public static fromJSON(json: any): TodoItem {
        return new TodoItem(
            json.id,
            json.title,
            json.completed ?? false,
            json.order
        );
    }

    public static toJSON(item: TodoItem): any {
        return {
            id: item.id,
            title: item.title,
            completed: item.completed,
            order: item.order
        };
    }
}