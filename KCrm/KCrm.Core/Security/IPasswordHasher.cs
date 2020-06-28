namespace KCrm.Core.Security {
    public interface IPasswordHasher {
        string Hash(string value);
        bool Match(string check, string hashed);
    }
}
