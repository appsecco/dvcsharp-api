# Damn Vulnerable CSharp: Core API

## Getting Started

Install .NET Core 2.x SDK
[Microsoft .NET Core SDK](https://www.microsoft.com/net/download/macos)

Install dependencies and migrate database:

```
dotnet restore
dotnet ef database update
```

Start application server:

```
dotnet run
```

Start application server with watcher:

```
dotnet watch run
```

## Vulnerabilities



## Issues (For Students to find and fix)

* Authentication is custom. It should be replaced with Identity Framework
* Hardcoded JWT secret and other validation info
* Weak password reset - same as DVJA
   * The reset link is never invalidated
* SSRF in Users import
* Authorization issue in User update in user controller/put method
   * Any user can elevate role to admin
   * Any user can reset other's credential


TODO:
   * Implement products model / CRUD
   * Implement product export using Xml Serializer (refer HackIM pwnable)
   * Implement serialization vulnerability in Product import
