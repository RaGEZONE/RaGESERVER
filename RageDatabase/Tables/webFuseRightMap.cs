using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;

namespace RageDatabase.Tables {
    
    
    public class webFuseRightMap : ClassMap<webFuseRight> {
        
        public webFuseRightMap() {
			Table("web_fuse_rights");
			LazyLoad();
			CompositeId();
			Map(x => x.@string).Column("string").Not.Nullable();
			Map(x => x.minimumRank).Column("minimum_rank").Not.Nullable();
        }
    }
}
