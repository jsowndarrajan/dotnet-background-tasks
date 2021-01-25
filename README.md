# Background Tasks

![.NET](https://github.com/jsowndarrajan/BackgroundTasks/workflows/.NET/badge.svg)

This is a demo project to explore different options avaialble  in .NET Framework for executing background tasks.

You will be required [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) to run this solution on your machine.

You can either run/debug the application in any of these IDEs:

* Visual Studio 2019
* Visual Studio Code

You can also execute the application in command prompt by using the below command:

> dotnet run

## What is background Task?

Background task runs behind the scenes and without any user intervention. It could be triggered based on time or executed after returning the response in an application.

### Examples

* Clean up database or file system every day
* Perform some CPU intensive work asynchronously
* Process messages from a queue every X minutes
* Refresh cache every X minutes
* Send payslip to employees every month
* Send performance reports to stakeholders every day

## Why do we need Hosted Service?

* The `IHostedService` background task execution is *coordinated* with the lifetime of the application
* You register tasks when the application starts, and you have the opportunity to do *graceful clean-up* when the application is shutting down.

## What are the options available?

* IHostedService
* BackgroundService
* WorkerService
* Hangfire
* Quartz
* Cloud alternatives
* and more

For more details about each option, please refer to [this](https://github.com/jsowndarrajan/BackgroundTasks/blob/master/Background%20Tasks.pdf) presentation slides (or) refer to my [sharing](https://youtu.be/3zL8zBPrfX0) session in the Azure Community Meetup, Singapore.
