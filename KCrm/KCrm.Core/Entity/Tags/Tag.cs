using System;

namespace KCrm.Core.Entity.Tags {
    public class Tag : BaseId, ISoftDelete, IChange {
        public string Name { get; set; }
        public Guid? TagGroupId { get; set; }
        public TagGroup TagGroup { get; set; } = null;
    }
}
