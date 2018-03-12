using System;
using KillBill.Client.Net.Configuration;
using KillBill.Client.Net.Data;
using KillBill.Client.Net.Implementations;
using KillBill.Client.Net.Interfaces;
using Microsoft.Extensions.Options;

namespace KillBill.Client.Net.Tests
{
    public class BaseTestFixture
    {
        private static readonly string _apiKey = "SyxKey";
        private static readonly Guid _tenantId = new Guid("c2b232b8-9938-4cee-9a38-e6b5e31d0a66");
        private static readonly Guid _accountId = new Guid("54a9653f-7295-4dfb-94bd-1df83097cc0d");
        private readonly Guid _bundleId = new Guid("554fe9de-b9e9-4321-ab71-791151944a91");
        private readonly KillBillClient _client;
        private readonly KillBillConfiguration _configuration;
        private readonly string _createdBy = "Testing User";
        private readonly string _reason = "KillBill Api Test";
        private readonly RequestOptions _requestOptions;
        private Random _random = new Random();

        public BaseTestFixture()
        {
            _configuration = new KillBillConfiguration
            {
                ServerUrl = "http://localhost:8080",
                ApiKey = _apiKey,
                ApiSecret = "SyxSecret",
                HttpUser = "admin",
                HttpPassword = "password"
            };

            _client = new KillBillClient(Configuration);

            _requestOptions = RequestOptions.Builder()
                                            .WithRequestId(Guid.NewGuid().ToString())
                                            .WithCreatedBy(CreatedBy)
                                            .WithReason(Reason)
                                            .WithUser(Configuration.HttpUser)
                                            .WithPassword(Configuration.HttpPassword)
                                            .WithTenantApiKey(Configuration.ApiKey)
                                            .WithTenantApiSecret(Configuration.ApiSecret)
                                            .WithComment("kill-bill-net-tests")
                                            .Build();
        }

        protected static string ApiKey
        {
            get { return _apiKey; }
        }

        protected static Guid TenantId
        {
            get { return _tenantId; }
        }

        protected static Guid AccountId
        {
            get { return _accountId; }
        }

        protected Guid BundleId
        {
            get { return _bundleId; }
        }

        protected KillBillClient Client
        {
            get { return _client; }
        }

        protected KillBillConfiguration Configuration
        {
            get { return _configuration; }
        }

        protected string CreatedBy
        {
            get { return _createdBy; }
        }

        protected string Reason
        {
            get { return _reason; }
        }

        protected RequestOptions RequestOptions
        {
            get { return _requestOptions; }
        }

        protected Random Random
        {
            get { return _random; }
        }
    }
}