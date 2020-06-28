using System;
using Newtonsoft.Json;

namespace KCrm.Logic.Services {
    public class AuthentiactedBaseRequest {
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public string Role { get; set; }
    }
}
