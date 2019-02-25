# searchKataRepo
Search Kata

# SearchKataApp 
## Setup

### Install msbuild 
### navigate to msbuild.exe 

installation may be in C:\Windows\Microsoft.NET\Framework\v4.0.30319 
Once installed, execute the following

```cmd.exe 
msbuild "C:\git\SearchKataApp\SearchKataApp\searchKataApp.sln" /t:Rebuild /p:Configuration=Release /p:Platform="any cpu"
```

### Install mstest 
### navigate to msbuild.exe 

installation may be in C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE
Once installed, execute the following 

```cmd.exe 
mstest /testcontainer:SearchKataUnitTests.dll
```

## Run
From the solution folder 

```cmd.exe 
cd SearchKataApp\bin\Release
SearchKataApp
```
