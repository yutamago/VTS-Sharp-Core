# VTS-Sharp-Core
This is a NuGet-wrapper for [VTS-Sharp](https://github.com/FomTarro/VTS-Sharp) that makes it compatible and easily usable within Non-Unity projects.

It also includes an [example VTS plugin](VTS.Example/Program.cs) that runs in the console instead of Unity.

## **Please refer to [VTS-Sharp](https://github.com/FomTarro/VTS-Sharp) for any documentation and questions on the API!** 

## NuGet
To use VTS-Sharp in your project, search for the package VTS-Sharp-Core in your IDE or use any of the following commands:

### .NET CLI
```bash
dotnet add package VTS-Sharp-Core
```
or
```bash
dotnet add package VTS-Sharp-Core --version 2.0.1.1
```

### Visual Studio Package Manager
```bash
NuGet\Install-Package VTS-Sharp-Core -Version 2.0.1.1
```

### PackageReference
```xml
<PackageReference Include="VTS-Sharp-Core" Version="2.0.1.1" />
```


## Example
*See [VTS.Example/](VTS.Example/) for details.*
```csharp
public static async Task Main(string[] args)
{
    var logger = new ConsoleVTSLoggerImpl();
    var websocket = new WebSocketNetCoreImpl(logger);
    var jsonUtility = new NewtonsoftJsonUtilityImpl();
    var tokenStorage = new TokenStorageImpl("");

    var plugin = new CoreVTSPlugin(logger, 100, "My first plugin", "My Name", "");

    try
    {
        await plugin.InitializeAsync(websocket, jsonUtility, tokenStorage, () => logger.LogWarning("Disconnected!"));
        logger.Log("Connected!");

        var apiState = await plugin.GetAPIStateAsync();
        logger.Log("Using VTubeStudio " + apiState.data.vTubeStudioVersion);
        
        var currentModel = await plugin.GetCurrentModelAsync();
        logger.Log("The current model is: " + currentModel.data.modelName);
    }
    catch (VTSException e)
    {
        logger.LogError(e);
    }
}
```
