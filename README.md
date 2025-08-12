# Interview
Hello, this is the C# technical interview for the position

## Setup
The code has been setup for you that migrates, and seeds a database, full with dependency injection and a generic host builder.
There are two service classes made for you, ClientService.cs and CRUDService.cs both services are already registered in dependency injection.
The ClientService has been completed for you, this code prompts a console input that will prompt the user for information based on a variety of inputs.
The code may not be the world's best, but we have to work with what we are given.

## Problem
The issue is the CRUDService is not completed. The Create, Read, Update, and Delete methods need to be implemented.
You need to implement these methods and confirm they work. You may (and it is preferred) to use the EntityFrameworkCore methods for interacting with the database. Only 1 method uses an actual SQL query.
The read update, called FetchJobData, is done for you. However, the engineer who wrote it made some mistakes in the SQL query. For this one, you will need to find the mistakes and fix them to correct the method.

## Evaluation
You will be evaluated on your ability and time it took to implement the methods. You may ask questions about the code. After implementing the code, you may be asked some questions. One thing to think about during the project is how you would change your code if the settings UseNoQueryTracking() was enabled on the database.

## Allowances
You may use the internet to search for documentation that can assist you in implementing these methods. We need to be resourceful as developers, and documentation is the #1 source for us to find information.

## Disallowances
You may not use AI. On the job you will not have any AI tooling readily available at first, and we will need you to perform as expected without it. This includes copying and pasting the code into Google/Bing and receiving the AI generated answer by Google/Bing. These answers are often inaccurate, and also a violation of company IP when on the job.
