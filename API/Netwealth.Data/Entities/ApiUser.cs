using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetWealth.Data.Entities
{
    public class ApiUser : EntityBase
    {

        public Guid Id { get; set; }
        public long UserId { get; set; } 
        public string Key { get; set; }

    }
}
