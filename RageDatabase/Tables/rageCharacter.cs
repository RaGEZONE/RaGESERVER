using Iesi.Collections.Generic; 
using System.Collections.Generic; 
using System.Text; 
using System; 


namespace RageDatabase.Tables {
    
    public class rageCharacter {
        public rageCharacter() { }
        public virtual int id { get; set; }
        public virtual string email { get; set; }
        public virtual string ticket { get; set; }
        public virtual string name { get; set; }
        public virtual int credits { get; set; }
        public virtual int pixels { get; set; }
        public virtual string regularIp { get; set; }
        public virtual string look { get; set; }
        public virtual string motto { get; set; }
        public virtual string gender { get; set; }
        public virtual string created { get; set; }
        public virtual string lastOnline { get; set; }
        public virtual int respect { get; set; }
        public virtual string online { get; set; }
        public virtual int home { get; set; }
    }
}
