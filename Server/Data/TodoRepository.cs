using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Data.Entities;

namespace Server.Data
{
    public class TodoRepository
    {
        private readonly DataContext _dataContext;
        public TodoRepository()
        {
            _dataContext = new DataContext();
        }

        public void CreateTodo(TodoEntity entity)
        {
            _dataContext.Todos.Add(entity);
            _dataContext.SaveChanges();
        }

        public void UpdateTodo(TodoEntity entity)
        {
            _dataContext.Todos.Update(entity);
            _dataContext.SaveChanges();
        }

        public TodoEntity? GetTodoById(string id)
        {
            var todo = _dataContext.Todos.FirstOrDefault(todo => todo.ID.ToString() == id);
            return todo;
        }

        public string? DeleteTodo(string id)
        {
            var todoToDelete = _dataContext.Todos.FirstOrDefault(todo => todo.ID.ToString() == id);

            if(todoToDelete is not null) {
                _dataContext.Todos.Remove(todoToDelete);
                _dataContext.SaveChanges();
                return id;
            }

            return null;
        }

        public List<TodoEntity> ListTodos()
        {
            return _dataContext.Todos.ToList();
        }
    }
}