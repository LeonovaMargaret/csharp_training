using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Configuration;

namespace WebAddressbookTests
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "AddressBook",
                        ProviderName = "MySql.Data.MySqlClient",
                        ConnectionString = @"Server=localhost;Port=3306;Database=addressbook;Uid=root;Pwd=;charset=utf8;"
                    };
            }
        }
    }
}
