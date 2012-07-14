using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using RageServer.Server.MySQL.Manager;
using java = biz.ritter.javapi; 

namespace RageServer
{
    class Program
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory sessionFactory;

        static void Main()
        {
            string connectionString = "localhost,port,user,password,maxpool,minpool";
            _sessionFactory = MySQLNHibernateCore.InstanceNewFactory(connectionString);
        }

        public static ISessionFactory Factory()
        {
            return _sessionFactory;
        }
    }
}
