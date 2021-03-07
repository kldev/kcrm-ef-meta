using System;
using Newtonsoft.Json;

namespace KCrm.Logic.Services {
    public class AuthentiactedBaseRequest {
        [JsonIgnore]
        
        internal Guid UserId { get;  private set; }
        [JsonIgnore]
        internal string Role { get; private set; }

        public void SetFromContext(Guid userId, string role) {
            this.UserId = userId;
            this.Role = role;
        }
    }
}
