using System;

namespace KCrm.Data.Aegis.Entity {
    public partial class Schemaversions {
        public int Schemaversionsid { get; set; }
        public string Scriptname { get; set; }
        public DateTime Applied { get; set; }
    }
}
