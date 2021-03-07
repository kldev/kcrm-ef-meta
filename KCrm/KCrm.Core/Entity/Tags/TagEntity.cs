using System;

namespace KCrm.Core.Entity.Tags {
    public class TagEntity : BaseGuidId, ISoftDelete, IChange {
        public string Name { get; set; }
        public Guid? TagGroupId { get; set; }
        public TagGroupEntity TagGroupEntity { get; set; } = null;
    }
}
