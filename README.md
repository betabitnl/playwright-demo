# playwright-demo

## Run on your machine
Open the solution in Visual Studio or 
```
dotnet restore
dotnet build
```

After that you should install the playwright stuff:
In Powershell run from the project dir:
```
pwsh .\bin\Debug\netX\playwright.ps1 install
```

Now you can run the tests!

## Azure DevOps pipeline
This includes a pipeline example!

[azure-pipelines.yml](playwright-demo/azure-pipelines.yml)
