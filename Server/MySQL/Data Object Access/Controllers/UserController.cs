using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RageServer.Server.MySQL.Data_Object_Access.Loader;
using RageDatabase.Tables;
using NHibernate;

namespace RageServer.Server.MySQL.Data_Object_Access.Controllers
{
    class UserController : DAOLoader<rageCharacter>
    {
        #region Fields/Properties
        public int Id { get; set; }
        public virtual string email { get; set; }
        public virtual string ticket { get; set; }
        public virtual string name { get; set; }
        public virtual int credits { get; set; }
        public virtual int pixels { get; set; } // Pixels removed.
        public virtual string regularIp { get; set; }
        public virtual string look { get; set; }
        public virtual string motto { get; set; }
        public virtual string gender { get; set; }
        public virtual string created { get; set; }
        public virtual string lastOnline { get; set; }
        public virtual int respect { get; set; }
        public virtual string online { get; set; }
        public virtual int home { get; set; }
        #endregion

        #region Constructor : Data
        /// <summary>
        /// Uses to get the data for the user, using the user's ID.
        /// </summary>
        /// <param name="Id">Id of user.</param>
        public UserController(int Id)
        {
            rageCharacter user;

            using (ISession s = Program.Factory().OpenSession())
            {
                user = s.Get<rageCharacter>(Id);

                this.email = user.email;
                this.ticket = user.ticket;
                this.name = user.name;
                this.credits = user.credits;
                this.regularIp = user.regularIp;
                this.look = user.look;
                this.motto = user.motto;
                this.gender = user.gender;
                this.created = user.created;
                this.lastOnline = user.lastOnline;
                this.respect = user.respect;
                this.online = user.online;
                this.home = user.home;
            }
            // No Id found.
            Console.WriteLine(string.Format("{0} Has not been found in the database!", Id));
        }
        #endregion

        #region DAO Methods [SaveOrUpdate, Delete]
        public void SaveOrUpdate(UserController currentInstance)
        {
            new DAOLoader<UserController>().SaveOrUpdate(currentInstance);

        }
        public void Delete(UserController currentInstance)
        {
            new DAOLoader<UserController>().Delete(currentInstance);
        }
        #endregion
    }
}
