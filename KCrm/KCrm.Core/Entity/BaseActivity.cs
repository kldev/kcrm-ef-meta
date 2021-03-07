using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KCrm.Core.Entity {
    public abstract class BaseActivity : BaseId<long> {
        public Guid UserId { get; set; }
        public string EventName { get; set; }
        public EventPayload EventData { get; set; }
    }

    public class EventPayload {
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("entity_name")]
        public string EntityName { get; set; } = string.Empty;
        
        [JsonPropertyName("entity_ids")]
        public List<Guid> EntityIDs { get; set; } = new List<Guid> ( );
    }
}
