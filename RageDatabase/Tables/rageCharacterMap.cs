using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace RageDatabase.Tables {
    
    
    public class rageCharacterMap : ClassMap<rageCharacter> {
        
        public rageCharacterMap() {
			Table("rage_characters");
			LazyLoad();
			CompositeId().KeyProperty(x => x.id, "id").KeyProperty(x => x.email, "e-mail").KeyProperty(x => x.ticket, "ticket");
			Map(x => x.name).Column("name").Not.Nullable();
			Map(x => x.credits).Column("credits").Not.Nullable();
			Map(x => x.pixels).Column("pixels").Not.Nullable();
			Map(x => x.regularIp).Column("regular_ip").Not.Nullable();
			Map(x => x.look).Column("look").Not.Nullable();
			Map(x => x.motto).Column("motto").Not.Nullable();
			Map(x => x.gender).Column("gender").Not.Nullable();
			Map(x => x.created).Column("created").Not.Nullable();
			Map(x => x.lastOnline).Column("last_online").Not.Nullable();
			Map(x => x.respect).Column("respect").Not.Nullable();
			Map(x => x.online).Column("online").Not.Nullable();
			Map(x => x.home).Column("home").Not.Nullable();
        }
    }
}
