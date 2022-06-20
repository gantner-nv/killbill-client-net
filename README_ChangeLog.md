# Change log
## Version 2.1.0
* Add Usage methods 

## Version 1.1.0
* Converted the IKillBillClient to work asynchronously. As a result all requests to the KillBill API will be executed async.

## Version 1.0.0
* Initial version of the dotnet core client library for [KillBill.io](http://killbill.io).

    The first version includes the basic functionalities for License management:
    * Tenants
    * Catalogs
    * Accounts
    * Subscriptions & Bundles
    
    Next to this, this version also includes a draft version for some other functionalities (these have not been tested throughly yet):
    * Invoices
    * Payments