const todoList = [];

// const desc = document.getElementById('description')
// desc.addEventListener('paste', (e) => {
//     console.log(e);
// })

function saveTodo() {
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
    }

    const descriptionElement = document.getElementById("description");

    if(descriptionElement) {
        todo.description = descriptionElement.value;
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


            todosQueryResult[0].appendChild(newTodoItem);
            todoList.push(todo);
        }
    }
}

// function titleChanged(e) {
//     const titleElementQueryResult = document.querySelectorAll("#title");

//     console.log(titleElementQueryResult);
    
// }


setTimeout(() => {
    const submitButtonQueryResult = document.querySelectorAll('.btn-submit');

    if(submitButtonQueryResult.length) {
        submitButtonQueryResult[0].addEventListener('click', saveTodo, false);
    }

    // const titleElementQueryResult = document.querySelectorAll("#title");

    // if(titleElementQueryResult.length) {
    //     titleElementQueryResult[0].addEventListener('change', (e) => {
    //         console.log(e.target.value);
    //         titleElementQueryResult[0].value = e.target.value;
    //     })
    // }

    
    // descriptionElement.addEventListener('paste', () => {
    //     descriptionElement.setAttribute('innerHTML', 'Text Pasted');
    //     descriptionElement.setAttribute('outerHTML', 'Text Pasted');
    //     console.log(description.value);
    // });
}, 3 * 1_000);