using System.Collections.Generic;

namespace KCrm.Core.Entity.Tags {
    public class TagGroupEntity : BaseGuidId, ISoftDelete, IChange {
        public string Name { get; set; }
        public ICollection<TagEntity> Tags { get; set; } = new List<TagEntity> ( );
    }
}
