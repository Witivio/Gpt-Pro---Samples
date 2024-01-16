# GPT Pro: Plugin Todo List

#### What this plugin do

When a GPT Pro user wants to add a task to their to-do list, they can simply formulate it using keywords such as:

- "I need to go to the hairdresser"
- "Add to my to-do list: go shopping"
- "Add the 10am meeting to my list"
- "Here is my shopping list: bread, milk, butter, eggs"

GPT Pro will then understand that it has to use the To-Do List API plugin and will automatically call the POST route, providing it with the necessary data. The plugin will insert the information on the given task into memory in the database.

If the user wants to know what tasks they still need to do, they can ask clearly:

- "What do I have left to do today?"
- "Can you show me my list of tasks to do?"

GPT Pro will know that it needs to call the To-Do List API plugin and will call the GET route to retrieve the list and transmit it to the user in natural language.

![Alt text](DiagramTodoList.png)

Please note, this plugin is an example and should not be used in production as is. The database is in memory, which is not suitable for extensive use.
on the other hand this plugin does not take into account single users and is therefore shared by all users.

#### How To Run This Sample in a local environment

##### Required Tools

- [Visual Studio](https://visualstudio.microsoft.com/fr/downloads/)
- [Ngrok](https://ngrok.com/) or any other tunneling too
- [Postman](https://www.postman.com/)

##### Launch the API

##### Step 1: Clone the repository

The first step is to clone the Plugin Microsoft Support repository from Github.
`git clone https://github.com/Witivio/Gpt-Pro---Samples`

Open the Plugin Microsoft Support project in Visual Studio or Visual Studio Code. Navigate to the `Samples` folder and open `Asp.net Core - Todo API.sln`.

##### Step 2: Launch the Plugin API

ngrok is used to create a public URL for the API.
Open a terminal and launch the folowing command from ngrok
`ngrok http https://localhost:7196 `

This will create a public URL that forwards traffic to the local port `https://localhost:7196 `. Copy the forwarding URL to use later.

Open the file located at `/.well-known/ai-plugin.json` and `openapi.json` and update the existing json value `servers:url` and `api:url` with the ngrok forwarding url you obtained earlier"

openapi.json
![Alt text](image-3.png)
ai-plugin.json
![Alt text](image-2.png)

Launch the api with Visual Studio, with F5 or click on :

##### Step 3: Test the API

Launch Postman and send a GET request to the Plugin API endpoint.
for this sample the endpoint is Todos

Write the GET request with the base domain optained in ngrok forwarding url and click on send button

**Example of query:** https://[yoursubdomain].ngrok.app/Todo

It should look like this in postman
![Alt text](image-1.png)
If it you're first call, the response must be empty.
now, add a todo to the todo list.
With Postman switch to POST method
and call this same query with a body like this:

```JSON
{
  "id": "1",
  "Title":  "Go shopping",
  "Description": "Milk, butter, biscuit, bacon",
  "IsDone": false
}
```

![Alt text](image.png)

You can add several todos to your todo list then come back to the GET method.
now, a todo list appears in the response

Don't forget that this is a demonstration plug-in, if you close and then restart the API, the data is lost

##### Tips and Documentation

- You can track calls to the API and debug easily with the ngrok web interface
  ![Alt text](image-4.png)
- To validate the description of your API, use [Swagger Validator](https://validator.swagger.io/).
- To learn more about the Swagger Specification, visit [swagger.io](https://swagger.io/specification/).

> #### :warning: Warning
>
> - A plugin name can contain only ASCII letters, digits, underscores and whitespace

:boom: :tada: Congratulations! You have successfully launched the GPT Pro Todo List Plugin API.
You can now begin creating custom plugins to extend the functionality of GPT Pro. :tada: :boom:
