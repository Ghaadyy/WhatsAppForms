# WhatsAppForms

A real-time WhatsApp clone made using C# and Windows Forms

I created this project as a final project for Introduction to Programming 2, a course in my Computer Science degree.

- Real-time communication between any two users, even if multiple users are connected, it maintains private communication between each conversation.
- The server stores all the data, I created a socket server that handles all received messages and other requests such as authentication or getting data.
- If the person sends a message to an offline user, the message gets sent and stored on the server, the recipient gets it when he logs in. If he is online he receives it immediately.
- All the data is sent using JSON, and it is stored in CSV file to act as a database on the server.

# Motivation

I wanted to go beyond the scope of the course _(Introduction to Programming 2)_, so I learned new topics in C# like Events & Delegates, Lambda Expressions, LINQ, and Multithreading.
Which I then utilized inside of this project as a practice.

## Installation steps

Inside the `bin/Debug/net7.0/` folder, add `Messages.csv`, `Users.csv` and `Conversations.csv`. These files act as the database of the application.

Change the IP Address inside the `SocketServer.cs` file located inside the `WhatsAppServer` folder to the IP Address of the machine running the code.

Run the client project and the server project and you're good to go!
