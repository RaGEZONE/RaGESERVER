using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using RageServer.Server.MySQL.Manager;
<<<<<<< HEAD
using java = biz.ritter.javapi;
using MySql.Data.MySqlClient;
using RageServer.Server.MySQL.Data_Object_Access.Controllers; 
=======
using Mango;
using java = biz.ritter.javapi; 
>>>>>>> 3c8c5d040ad31e6704947f980c429df5f011beea

namespace RageServer
{
    class Program
    {
        private static ISessionFactory _sessionFactory;

<<<<<<< HEAD
        static void Main()
        {
            
            _sessionFactory = MySQLNHibernateCore.InstanceNewFactory(connectionString("localhost", 3306, "root", "zaknonrp"));

            Console.WriteLine("Connected to Database");

            // Just to see if it trys to get the Id 1
            //UserController test = new UserController(1);

            Console.Read();
=======
       // private static ISessionFactory sessionFactory;

        

        static void Main()
        {
            string connectionString = "localhost,port,user,password,maxpool,minpool";
            _sessionFactory = MySQLNHibernateCore.InstanceNewFactory(connectionString);
            
>>>>>>> 3c8c5d040ad31e6704947f980c429df5f011beea
        }

        public static ISessionFactory Factory()
        {
            return _sessionFactory;
        }

        private static string connectionString(string host, uint port, string username, string password)
        {
                MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();

                sb.Server = host;
                sb.Port = port;
                sb.UserID = username;
                sb.Password = password;
                sb.MaximumPoolSize = 10;
                sb.MinimumPoolSize = 0;
            
                return sb.ToString();
        }
    }
}
