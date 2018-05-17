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

TODO:

   Fix the XML deseralization vulnerability, refer to:
   https://github.com/abhisek/pwnworks/blob/master/challenges/dotnet-deserialization/restapp/Program.cs

   Move import to imports controller for generic XML serialized object importing with typename being controlled by attacker.

* Authentication is custom. It should be replaced with Identity Framework
* Authentication bypass through GetTokenSSO / Authorization controller
* Hardcoded JWT secret and other validation info
* Weak password reset - same as DVJA
   * The reset link is never invalidated
* SSRF in Users import
* Authorization issue in User update in user controller/put method
   * Any user can elevate role to admin
   * Any user can reset other's credential
* XML serialization - unsafe (ysoserial)
   * Products controler import/export
