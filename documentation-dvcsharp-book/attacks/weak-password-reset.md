# Weak Password Reset

## Overview

It is possible to reset password of arbitrary user in the system, identified by their email address due to a flaw in the password reset implementation.

## Exploitation

The password reset flow is executed in 2 step process:

1. Request for password reset
2. Use the password reset link obtained from the system to reset user's own password

The endpoint below handles password reset request and password request submission:

```
http://rws.local:5000/api/passwordresets
```

The following request can be sent to initiate a password reset request, by requesting a password reset token:

```
curl -X POST \
  http://rws.local:5000/api/passwordresets \
  -H 'Content-Type: application/json' \
  -d '{
	"email": "test@test.com"
}'
```

The application responds with a message indicating that the password reset link is sent to registered email address:

```
An email with password reset link has been sent.
```

However, it turns out that the password reset request only contains a `key` parameter that can be derived from the e-mail address. 

The password reset can be executed by sending a `PUT` request with the following payload to the same API endpoint:

```json
{
	"key": "b642b4217b34b1e8d3bd915fc65c4452",
	"password": "new-password",
	"passwordConfirmation": "new-password"
}
```

The `key` value is used to identify the password reset request initiated for a given user. It was found that the `key` value is just `MD5` checksum of user's email address, which can easily be computed by an attacker.

## Impact

This issue can be use to reset and take over any registered user account identified by their e-mail address.