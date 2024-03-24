using System.Collections.Generic;
using Server.Data.Entities;

namespace Server.Data.Interfaces
{
    public interface ITodoRepository
    {
        void CreateTodo(TodoEntity entity);
        string? DeleteTodo(string id);
        TodoEntity? GetTodoById(string id);
        List<TodoEntity> ListTodos();
        void UpdateTodo(TodoEntity entity);
    }
}