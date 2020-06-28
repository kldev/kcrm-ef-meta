namespace KCrm.Core.Security {
    public class BCryptPasswordHasher : IPasswordHasher {
        public string Hash(string value) {
            return BCrypt.Net.BCrypt.HashPassword (value);
        }

        public bool Match(string check, string hashed) {
            return BCrypt.Net.BCrypt.Verify (check, hashed);
        }
    }
}
