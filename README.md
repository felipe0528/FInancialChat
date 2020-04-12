# Readme

## Prerequisites
1. [Erlang OTP 22.3](https://www.erlang.org/downloads) 
2. [RabbitMQ Server](https://www.rabbitmq.com/download.html)
3. Microsoft .Net 4.7
4. Internet Information Services(IIS) V. 10.0

## About
In this project, I implemented a web application that enables chat communication between registered users and gives the ability to use a command to call a bot and receive stock prices in the chat, also the chat shows a timestamp per message and saves in RabbitMQ server 50 messages per user when they are not read.

Things that I used / Implemented:
RabbitMQ, MVC, .NET Framework 4.7, VS Unit Testing.

The application is using .NET Identity. If the user types a wrong stock code the app will send back in the chat a message with the alert, the app ignores the bot if the user spells wrong the command. The code to create the installer is in the repository. 

## Installation
1. Run WebApp.msi with admin privileges (Ask Me to send the installer to you)
2. Accept Agreement
3. Select Destination Folder
4. Install

## Usage
1. In your browser go to [http://localhost:8080/Account/Register](http://localhost:8080/Account/Register)
2. Create an account
4. Start Chating
