# .NET Deriv API Library

## The Unofficial Deriv Api Client to work with the awesome trading platform

This repository contains a library to access, consume and work with Deriv API Trading Platform
using dotnet tecnol

Now I explain what this library consists of and what its advantages are.
- The first thing is to abstract from knowing the json schema of the
requests and the use of websocket itself
providing a unified API to carry out tasks, consult information and stream
information in real time as ticks or last prices of a contract.
- Api Fluent as a template to build contracts, consult the Profit Table,
the data history, etc.
- Ease of buying and selling contracts by calling a few methods.
- A variety of methods to consult historical data.
- Resilience policies to manage eventualities (such as connection drop) of
the websocket connection.
- Tasks of request mappings, stream management and other json serialization
processes are performed behind the methods
so using this library frees developers from these things.

## Contributing

We would love community contributions here.

The moment is necesary documnetation and test code coverage

This project has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/) 
to clarify expected behavior in our community.

## License

This project is licensed with the [MIT license](LICENSE).