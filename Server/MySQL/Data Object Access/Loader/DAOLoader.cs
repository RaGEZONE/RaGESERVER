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
        public Task Async()
        {
            return Task.Delay(1000);
        }

        public async void SaveOrUpdate(T item)
        {
            // Wait 10 seconds before a start of async.
            await Async();

            using (var session = Program.Factory().OpenSession())
            {
                session.SaveOrUpdate(item);
                session.Transaction.Commit();
            }
        }

        public async void Delete(T item)
        {
            await Async();
            using (var session = Program.Factory().OpenSession())
            {
                session.Delete(item);
                session.Transaction.Commit();
            }
        }

        public T SetData(int id)
        {
            using (var session = Program.Factory().OpenSession())
            {
                return session.Get<T>(id);
            }
        }
    }
}
