Overview
===
An ASP.NET Core client library for [Killbill.io](http://killbill.io).

The client library is currently built in **netstandard2.0**.

Background
===
This library was forked from the original draft & prototype version which is available on https://github.com/galenp/killbill-client-net.

We converted the library to ASP.NET Core + upgraded, improved and refactored this library so we can make use of it correctly and efficiently.

Keep in mind that not all functionalities of KillBill.io are implemented (yet), so be my guest to contribute...

Getting started
===
To make use of Killbill methods, we first need to create the configuration.

    var configuration = new KillBillConfiguration
    {
        ServerUrl = "http://localhost:8080",
        ApiKey = "mykey",
        ApiSecret = "mysecret",
        HttpUser = "admin",
        HttpPassword = "password"
    };

Next, we create a KillBillClient object based on that configuration.

    var client = new KillBillClient(Configuration);

Before we can start making use of the Killbill interface, we can easily create a generic RequestOptions object by making use of the extension methods. This is optional, but can save some time...

    var requestOptions = RequestOptions.Builder()
        .WithRequestId(Guid.NewGuid().ToString())
        .WithCreatedBy(CreatedBy)
        .WithReason(Reason)
        .WithUser(Configuration.HttpUser)
        .WithPassword(Configuration.HttpPassword)
        .WithTenantApiKey(Configuration.ApiKey)
        .WithTenantApiSecret(Configuration.ApiSecret)
        .WithComment("kill-bill-test")
        .Build();

And now we can start using it by simply calling the (async) method(s) we need.
Eg. find an account by id:

    var account = await Client.GetAccount(accountId, requestOptions);

History
===
See [Change log](README_ChangeLog.md) for all documented changes and version history.