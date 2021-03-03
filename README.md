# Stackstorm.Api Readme

These class library projects connect Crucible systems to Stackstorm via the Stackstorm API. The main components are as follows:

- **Stackstorm.Connector:** A formatted connector that gives defined objects in and out of a given method.
- **Stackstorm.Api.Client:** A raw connector to Stackstorm API. You must know parameter dictionary values.

This project produces nuget packages [Stackstorm.Api.Client](https://www.nuget.org/packages/Stackstorm.Api.Client) and [Stackstorm.Connector](https://www.nuget.org/packages/Stackstorm.Connector/) that are consumed by the [Steamfitter.Api](https://github.com/cmu-sei/Steamfitter.Api) project.

## Reporting bugs and requesting features

Think you found a bug? Please report all Crucible bugs - including bugs for the individual Crucible apps - in the [cmu-sei/crucible issue tracker](https://github.com/cmu-sei/crucible/issues). 

Include as much detail as possible including steps to reproduce, specific app involved, and any error messages you may have received.

Have a good idea for a new feature? Submit all new feature requests through the [cmu-sei/crucible issue tracker](https://github.com/cmu-sei/crucible/issues). 

Include the reasons why you're requesting the new feature and how it might benefit other Crucible users.

## License

Copyright 2021 Carnegie Mellon University. See the [LICENSE.md](./LICENSE.md) files for details.
