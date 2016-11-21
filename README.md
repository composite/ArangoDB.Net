# ArangoDB.Net
(WIP) ArangoDB Client for .NET with VelocyPack

Based from official [arangodb/arangodb-java-driver](https://github.com/arangodb/arangodb-java-driver)

for using `VelocyStream` protocol instead of `HTTP`.

## Status

- ArangoDB.NET Migration in progress but VelocyPack must complete first. 
- VelocyPack Build successful, but many errors in unit tests.

## Dependencies

- ArangoDB.NET -> VelocyPack
- VelocyPack -> [Json.NET](https://github.com/JamesNK/Newtonsoft.Json)

## TODO

- `VelocyPack` must be passed all unit test!<br>
  (11 passed / 12 failed / 23 implemented / 262 all tests in velocystream)
- `async` and `Linq` support.

## Why did you open source with this?

It's just help using ArangoDB client with `VelocyStream` protocol for .NET developers.
when migrated `VelocyStream` successfully, I'll choose which next plan, .NET Driver migration or help existing .NET drivers.

## This solution contains only .NET Core projects. why?

yes. I'm working on .NET standard environment. but I have a plan that makes support on:

- .NET Standard 1.1
- Microsoft .NET 4.0 or above
- Windows 8, 8.1, 10 universal
- Xamarin (but I hate Mono.)
- No plan for silverlight.

*(NOTE:VelocyPack only. not included ArangoDB.NET with supporting env yet.)*

## License

Same as `arangodb/arangodb-java-driver`, Apache License 2.0