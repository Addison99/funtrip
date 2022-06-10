using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObject.Models
{
    public partial class AreaGroup
    {
        public AreaGroup()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int AreaId { get; set; }
        public int GroupId { get; set; }
        public string Status { get; set; }

        public virtual Area Area { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
