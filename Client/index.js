let todoList = [];
let isUpdate = false;

const todoToUpdate = {};
const baseURL = 'http://localhost:5272';


async function saveTodo() {
    if(isUpdate) {
        await updateTodo();
    } else {
        createTodo();
    }
}

const createTodo = () => {
    let todo = {
        title: '',
        description: ''
    };

    const titleElement = document.getElementById("title");

    if(titleElement) {
        const todoIndex = todoList.findIndex((t) => t.title == titleElement.value);

        if(todoIndex >= 0) {
            return;
        }

        todo.title = titleElement.value;

        titleElement.value = '';
    }

    const descriptionElement = document.getElementById("description");

    if(descriptionElement) {
        todo.description = descriptionElement.value;

        descriptionElement.value = '';
    }
    

    if(todo.title) {
        const todosQueryResult = document.querySelectorAll('.todo-list');

        if(todosQueryResult.length) {
            var newTodoItem = document.createElement('li');

            var todoItemTitle = document.createElement('p');
            todoItemTitle.innerHTML = todo.title;
            
            newTodoItem.appendChild(todoItemTitle);

            var todoItemDescription = document.createElement('span');
            todoItemDescription.innerHTML = todo.description;
            
            newTodoItem.appendChild(todoItemDescription);

            var editButton = document.createElement('button');
            editButton.innerHTML = 'Edit';

            editButton.addEventListener('click', () => editTodoItem(newTodoItem));

            newTodoItem.appendChild(editButton);

            todosQueryResult[0].appendChild(newTodoItem);
            todoList.push(todo);
        }
    }
};

const updateTodo = async () => {
    isUpdate = false;

    const allTodos = document.querySelectorAll('li');
    // console.log({allTodos});

    allTodos.forEach((todo) => {
        let foundTodo = false;
        todo.childNodes.forEach((child, i) => {
            if(child.id === 'todo_id') {
                if(child.innerHTML === todoToUpdate.id) {
                    foundTodo = true;
                    // console.log(child.innerHTML)
                    // todoToUpdate.id = child.innerHTML;
                }
            }

            if(child.id === 'todo_title' && foundTodo) {
                const titleElement = document.getElementById("title");
                // child.innerHTML = titleElement.value;
                // console.log(titleElement.value);
                todoToUpdate.title = titleElement.value;
                titleElement.value = '';
            }

            if(child.nodeName === 'SPAN' && foundTodo) {
                const descriptionElement = document.getElementById("description");
                // child.innerHTML = descriptionElement.value;
                todoToUpdate.description = descriptionElement.value;
                // console.log(descriptionElement.value);
                descriptionElement.value = '';

                foundTodo = false;
            }
        })
    });

    console.log(todoToUpdate);

    // Make API call to perform update
    try 
    {
        const updateResponse = await fetch(`${baseURL}/todo/update`, {
            method: 'PUT',
            body: JSON.stringify(todoToUpdate),
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const responseBody = await updateResponse.json();
        await listTodos();
        console.log(responseBody);
    } catch (error) {
        console.error(error);
    }
};

function editTodoItem(todoLiElement) {
    // console.log('Todo item clicked ', el);
    const titleElement = document.getElementById("title");
    const descriptionElement = document.getElementById("description");

    // titleElement.value = 

    todoLiElement.childNodes.forEach((child) => {
        if(child.id === 'todo_id') {
            titleElement.value = child.innerHTML;
            todoToUpdate.id = child.innerHTML;
        }

        if(child.id === 'todo_title') {
            titleElement.value = child.innerHTML;
            todoToUpdate.title = child.innerHTML;
        }

        if(child.nodeName === 'SPAN') {
            descriptionElement.value = child.innerHTML
            todoToUpdate.description = child.innerHTML;
        }
    });

    isUpdate = true;
}

const listTodos = async () => {
    const response = await fetch(`${baseURL}/todo/list`);
    todoList = await response.json();

    const todosQueryResult = document.querySelectorAll('.todo-list');

    
    if(todosQueryResult.length <= 0) {
        return;
    }

    const todoBoxQueryResult = document.querySelectorAll('.todo-box');

    if(!todoBoxQueryResult.length) {
        return;
    }

    todoBoxQueryResult[0].removeChild(todosQueryResult[0]);



    const todoListElement = document.createElement('ol');
    todoListElement.className = 'todo-list';

    todoList.forEach(({ id, title, description }) => {
        var newTodoItem = document.createElement('li');

        var todoItemId = document.createElement('p');
        todoItemId.innerHTML = id;
        todoItemId.id = 'todo_id'
        todoItemId.style.display = 'none';

        newTodoItem.appendChild(todoItemId);

        var todoItemTitle = document.createElement('p');
        todoItemTitle.innerHTML = title;
        todoItemTitle.id = 'todo_title'
        
        newTodoItem.appendChild(todoItemTitle);

        var todoItemDescription = document.createElement('span');
        todoItemDescription.innerHTML = description;
        
        newTodoItem.appendChild(todoItemDescription);

        var editButton = document.createElement('button');
        editButton.innerHTML = 'Edit';

        editButton.addEventListener('click', () => editTodoItem(newTodoItem));

        newTodoItem.appendChild(editButton);

        todoListElement.appendChild(newTodoItem);
    })

    todoBoxQueryResult[0].appendChild(todoListElement);
}

// function titleChanged(e) {
//     const titleElementQueryResult = document.querySelectorAll("#title");

//     console.log(titleElementQueryResult);
    
// }


setTimeout(async () => {
    const submitButtonQueryResult = document.querySelectorAll('.btn-submit');

    if(submitButtonQueryResult.length) {
        submitButtonQueryResult[0].addEventListener('click', () => saveTodo());
    }

    try 
    {
        await listTodos();
    } catch(error) {
        console.error(error.message)
    }
    
}, 3 * 1_000);