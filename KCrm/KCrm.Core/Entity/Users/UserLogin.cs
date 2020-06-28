using System;

namespace KCrm.Core.Entity.Users {
    public class UserLogin : BaseId, IChange {
        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
        public TimeSpan ValidPeriod { get; set; }
        public DateTime? Created { get; set; }
    }
}
