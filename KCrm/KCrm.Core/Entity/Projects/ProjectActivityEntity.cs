using System;

namespace KCrm.Core.Entity.Projects {
    public class ProjectActivityEntity : BaseActivity, IChange {
        public Guid ProjectId { get; set; }
    }
}
