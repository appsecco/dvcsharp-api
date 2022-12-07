# Email Brute Force

The password reset API endpoint available at the below URL can be exploited to identify if a given email address is registered with the system:

```
http://rws.local:5000/api/passwordresets
```

This is due to difference in response for registered and unregistered e-mail address.

The system responds with indicative message when a password reset token is requested for an email address that does not exist.

```bash
curl -X POST \
  http://rws.local:5000/api/passwordresets \
  -H 'Content-Type: application/json' \
  -d '{
	"email": "doesnotexists@test.com"
}'

{
    "email": [
        "Email address does not exist"
    ]
}
```

However, when the password reset token is requested for a valid e-mail address, the system responds with a message confirming the same.

```
An email with password reset link has been sent.
```

This difference in response can be exploited to identify if a given email address is already registered with the system. An attacker can leverage this issue to identify registered email addresses through brute force attack.