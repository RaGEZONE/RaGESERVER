using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RageServer.Server.MySQL.Data_Object_Access.Interface
{
    interface IDAO<T>
    {
        void SaveOrUpdate(T entity);
        void Delete(T entity);
    }
}
