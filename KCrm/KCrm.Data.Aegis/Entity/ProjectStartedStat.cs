using System;
using System.Collections.Generic;

#nullable disable

namespace KCrm.Data.Aegis.Entity
{
    public partial class ProjectStartedStat
    {
        public int? Monthnumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public long? Count { get; set; }
    }
}
