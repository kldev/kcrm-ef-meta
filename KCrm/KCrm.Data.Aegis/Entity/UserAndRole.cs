using System;
using System.Collections.Generic;

#nullable disable

namespace KCrm.Data.Aegis.Entity
{
    public partial class UserAndRole
    {
        public Guid? Id { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? UserId { get; set; }
        public string Roles { get; set; }
        public string Topusers { get; set; }
    }
}
