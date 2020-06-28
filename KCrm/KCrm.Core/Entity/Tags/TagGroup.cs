using System.Collections.Generic;

namespace KCrm.Core.Entity.Tags {
    public class TagGroup : BaseId, ISoftDelete, IChange {
        public string Name { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag> ( );
    }
}
