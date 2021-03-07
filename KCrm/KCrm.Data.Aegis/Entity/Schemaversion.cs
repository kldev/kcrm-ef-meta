using System;
using System.Collections.Generic;

#nullable disable

namespace KCrm.Data.Aegis.Entity
{
    public partial class Schemaversion
    {
        public int Schemaversionsid { get; set; }
        public string Scriptname { get; set; }
        public DateTime Applied { get; set; }
    }
}
