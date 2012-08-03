
using System.Collections.Generic; 
using System.Text; 
using System; 


namespace RageDatabase.Tables {
    
    public class webUser {
        public webUser() { }
        public virtual int id { get; set; }
        public virtual string email { get; set; }
        public virtual string password { get; set; }
        public virtual string language { get; set; }
    }
}
