using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace RageDatabase.Tables {
    
    
    public class webUserMap : ClassMap<webUser> {
        
        public webUserMap() {
			Table("web_users");
			LazyLoad();
			Id(x => x.id).GeneratedBy.Identity().Column("id");
			Map(x => x.email).Column("e-mail").Not.Nullable();
			Map(x => x.password).Column("password").Not.Nullable();
			Map(x => x.language).Column("language").Not.Nullable();
        }
    }
}
