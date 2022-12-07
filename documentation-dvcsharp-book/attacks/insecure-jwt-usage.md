# Insecure JWT Usage

The following vulnerabilities were found through source code review:

1. Hardcode JWT secret in `User` model class
2. Failure to verify `audience`, `issuer` in authentication middleware configuration

The first issue exists in `User.cs`:

```c
[...]
      public const string RoleSupport = "Support";
      public const string RoleAdministrator = "Administrator";
      public const string TokenSecret = "f449a71cff1d56a122c84fa478c16af9075e5b4b8527787b56580773242e40ce";
      public int ID { get; set; }
[...]
```

The second exists in `Startup.cs`:

```c
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options => {
      options.TokenValidationParameters = new TokenValidationParameters
      {
         ValidateIssuer = false,
         ValidateAudience = false,
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
               GetBytes(Models.User.TokenSecret))
      };
   });
```

## Impact

This results in an insecure environment as the `secret` token is available in CI/CD, developer laptops. This increases the overall attack surface for `secret` compromise. If an attacker manages to get the JWT secret, then he will be able to craft his own signed JWT which will be accepted by the system. 

This will result in authentication bypass and privilege elevation.