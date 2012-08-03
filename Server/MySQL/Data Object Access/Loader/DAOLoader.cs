using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RageServer.Server.MySQL.Data_Object_Access.Interface;

namespace RageServer.Server.MySQL.Data_Object_Access.Loader
{
    class DAOLoader<T> : IDAO<T>
    {

        public void SaveOrUpdate(T item)
        {
           
            using (var session = Program.Factory().OpenSession())
            {
                session.SaveOrUpdate(item);
                session.Transaction.Commit();
            }
        }

        public void Delete(T item)
        {            
            using (var session = Program.Factory().OpenSession())
            {
                session.Delete(item);
                session.Transaction.Commit();
            }
        }
    }
}
