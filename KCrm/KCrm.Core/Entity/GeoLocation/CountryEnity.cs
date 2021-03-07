namespace KCrm.Core.Entity.GeoLocation {
    public class CountryEntity : BaseGuidId {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Iso { get; set; }
    }
}
