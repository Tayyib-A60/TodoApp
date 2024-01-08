const todoList = [];

function saveTodo() {
    let todo = {
        title: '',
        description: ''
    };

    const titleElement = document.getElementById("title");

    if(titleElement) {
        todo.title = titleElement.value;
    }

    const descriptionElement = document.getElementById("description");

    if(descriptionElement) {
        todo.description = descriptionElement.value;
    }

    todoList.push(todo);

    console.log(todoList);
}