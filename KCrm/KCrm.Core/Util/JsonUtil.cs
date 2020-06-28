using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace KCrm.Core.Util {
    public static class JsonUtil {
        public static string SerializeObject(object obj) {
            var contractResolver = new DefaultContractResolver {
                NamingStrategy = new CamelCaseNamingStrategy {
                    OverrideSpecifiedNames = false
                }
            };

            var json = JsonConvert.SerializeObject (obj, new JsonSerializerSettings {
                ContractResolver = contractResolver,
                Formatting = Formatting.None
            });

            return json;
        }

        public static T DeserializeObject<T>(string value) {
            return JsonConvert.DeserializeObject<T> (value);
        }
    }
}
