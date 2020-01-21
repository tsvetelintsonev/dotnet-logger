# Introduction 
You have been handed this project containing a logger which is supposed to write log statements asynchronously to a sink. However it does not seem to work asynchronously... The project is meant as an in-house replacement for `log4net` and `Serilog`. The initial contributor is no longer with us, so you now get the task.
Below is a prioritized backlog. 

# Backlog

1. Refactor Logger to be as fast as possible, so the caller can get on with its work and not wait for the log statement to be written. This can be verified by running the test `IsLoggingFast`
    *  However due to the fact that the logger is currently used in other systems, the current Write method can not have its signature changed
2. Make sure the logger  never crashes the calling application due to errors
3. Make sure the code is covered by relevant unit tests
4. Enrich log statements with timestamp and log level (debug, Information, Warning, Error)
5. Implement a file sink
    * New file should be created at midnight
6. Refactor AsyncLogger to be able to use multiple sinks

# Outcome
Complete as many of the above tasks as possible within your timeframe. Focus should be on quality rather than quantity. Together with the coded solution, a small text should be provided with

* Explanations for chosen solutions
* Perspectives on what can be implemented in the future (i.e types or sinks or other extensions to the functionality)

Feel free to be creative and complete the tasks how you see fit.

# Notes
* Try to make the code as SOLID as possible (https://en.wikipedia.org/wiki/SOLID)


# Solution

## Design choices
Solution's API and overall design is following the SOLID principles, where each entity is responsible for only one area of the system's functionality which ensures good levels of low coupling and high cohesion.

The 3 main areas of responsibilities in the solution are:
- Logging API (Facade) - provided by the `Logger`
- Log statement dispatching - currently only async via `AsyncLogStatementDispatcher`
- Log statement writes - currently only via `RollingFileSink`

The current implementation doesn't utilize an IoC container, but since all dependencies between the system's entities goes through a 'controller' injections, integrating such container should be a straight forward operation.

## Async support
The solution is utilizing a single `Task` for asynchronous log statement writes to a file through the `RollingFileSink`. The reason behind this is that a `Task` won't add much overhead to the application using the logger, because unlike a `Thread` it doesn't require its own stack allocation and kernel resources. 

A thread-safe `BlockingCollection` is used for storing log statements in `AsyncLogStatementDispatcher` in order to support concurrent log statement reads and writes. A typical use case scenario where this would be useful is a multi-threaded GUI application.

## Future perspectives
Here are a few interesting sinks that could potentially be implemented:
- Azure LogAnalytics
- Amazon CloudWatch
- ElasticSearch
- Humio
- DataDog
- Various NoSQL databases

One possibly useful functionality extension would be to add an option for custom log statement enrichers.
Another interesting functionality might be choosing between log statement output formats. This could be done by extending the `Logger` to support adding a collection of `ILogStatementRenderer` e.g. `JsonLogStatementRenderer` etc.

All these and any other future functionalities would eventually cause the `Logger` constructor to grow a lot and become a so called `fat controller`. One way this could be avoided is to introduce a fluent `Builder` for the `Logger`.

An `IDisposable` support could be considered, should a greater control over resources becomes a priority.

# Note
A `FuturePerspectives` project has been added containing many of the ideas for the future functionalities.
