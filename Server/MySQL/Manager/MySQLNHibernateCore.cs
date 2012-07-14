using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace RageServer.Server.MySQL.Manager
{
    class MySQLNHibernateCore
    {
        public static ISessionFactory InstanceNewFactory(string connstring)
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(connstring))
                .Cache(c => c.UseQueryCache().UseSecondLevelCache().UseMinimalPuts())

                .BuildSessionFactory();
        }
    }
}
