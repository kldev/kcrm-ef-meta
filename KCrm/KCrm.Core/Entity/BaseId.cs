using System;

namespace KCrm.Core.Entity {
    public abstract class BaseGuidId : BaseId<Guid> {
    }
    
    public abstract class BaseId<T> {
        public T Id { get; set; }
    }
}
