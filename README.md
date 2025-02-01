# Summary

This is a project to show a basic set of API tests which use C# and xUnit. The tests print out HTTP requests in cURL format (so they can be run manually for investigation purposes if necessary) and dynamically deserialise response JSON objects.

# Running the tests

Run the tests via the Visual Studio UI. Alternatively, to run the tests via the command line, execute the following command:
```
dotnet test --logger "trx;LogFileName=test-results.trx"
```

View the test results by opening the csharp-api-xunit/TestResults/test-results.trx file in Visual Studio.