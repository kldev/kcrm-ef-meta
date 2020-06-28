namespace KCrm.Logic.Config {
    public class AuthConfig {
        public string Secret { get; set; }

        public bool IsValid => !string.IsNullOrEmpty (Secret);
    }
}
