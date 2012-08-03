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
                //.Mappings(m => m.FluentMappings.Add<RageDatabase.Tables.rageCharacter>())
                .BuildSessionFactory();
        }
    }
}


/*
FluentNHibernate.Cfg.FluentConfigurationException was unhandled
  HResult=-2146233088
  Message=An invalid or incomplete configuration was used while creating a SessionFactory. Check PotentialReasons collection, and InnerException for more detail.


  Source=FluentNHibernate
  StackTrace:
       at FluentNHibernate.Cfg.FluentConfiguration.BuildSessionFactory() in d:\Builds\FluentNH-v1.x-nh3\src\FluentNHibernate\Cfg\FluentConfiguration.cs:line 232
       at RageServer.Server.MySQL.Manager.MySQLNHibernateCore.InstanceNewFactory(String connstring) in C:\Users\Lol At This Username\Documents\Visual Studio 2010\Projects\RageServer\RageServer\Server\MySQL\Manager\MySQLNHibernateCore.cs:line 15
       at RageServer.Program.Main() in C:\Users\Lol At This Username\Documents\Visual Studio 2010\Projects\RageServer\RageServer\Server\Program.cs:line 19
       at System.AppDomain._nExecuteAssembly(RuntimeAssembly assembly, String[] args)
       at System.AppDomain.ExecuteAssembly(String assemblyFile, Evidence assemblySecurity, String[] args)
       at Microsoft.VisualStudio.HostingProcess.HostProc.RunUsersAssembly()
       at System.Threading.ThreadHelper.ThreadStart_Context(Object state)
       at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state, Boolean preserveSyncCtx)
       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
       at System.Threading.ThreadHelper.ThreadStart()
  InnerException: FluentNHibernate.Cfg.FluentConfigurationException
       HResult=-2146233088
       Message=An invalid or incomplete configuration was used while creating a SessionFactory. Check PotentialReasons collection, and InnerException for more detail.


       Source=FluentNHibernate
       StackTrace:
            at FluentNHibernate.Cfg.FluentConfiguration.BuildConfiguration() in d:\Builds\FluentNH-v1.x-nh3\src\FluentNHibernate\Cfg\FluentConfiguration.cs:line 261
            at FluentNHibernate.Cfg.FluentConfiguration.BuildSessionFactory() in d:\Builds\FluentNH-v1.x-nh3\src\FluentNHibernate\Cfg\FluentConfiguration.cs:line 227
       InnerException: System.InvalidOperationException
            HResult=-2146233079
            Message=Unsupported mapping type 'RageDatabase.Tables.rageCharacter'
            Source=FluentNHibernate
            StackTrace:
                 at FluentNHibernate.PersistenceModel.Add(Type type) in d:\Builds\FluentNH-v1.x-nh3\src\FluentNHibernate\PersistenceModel.cs:line 152
                 at FluentNHibernate.Cfg.FluentMappingsContainer.Apply(PersistenceModel model) in d:\Builds\FluentNH-v1.x-nh3\src\FluentNHibernate\Cfg\FluentMappingsContainer.cs:line 127
                 at FluentNHibernate.Cfg.MappingConfiguration.Apply(Configuration cfg) in d:\Builds\FluentNH-v1.x-nh3\src\FluentNHibernate\Cfg\MappingConfiguration.cs:line 85
                 at FluentNHibernate.Cfg.FluentConfiguration.BuildConfiguration() in d:\Builds\FluentNH-v1.x-nh3\src\FluentNHibernate\Cfg\FluentConfiguration.cs:line 249
            InnerException: 

*/