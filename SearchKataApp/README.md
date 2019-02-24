# SearchKataApp 
## Setup

### Install msbuild 
### navigate to msbuild.exe 

installation may be in C:\Windows\Microsoft.NET\Framework\v4.0.30319 
Once installed, execute the following

```cmd.exe 
msbuild "C:\git\SearchKataApp\SearchKataApp\searchKataApp.sln" /t:Rebuild /p:Configuration=Release /p:Platform="any cpu"
```

## Run
From the solution folder 

```cmd.exe 
cd SearchKataApp\bin\Release
SearchKataApp
```