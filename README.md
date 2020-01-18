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


