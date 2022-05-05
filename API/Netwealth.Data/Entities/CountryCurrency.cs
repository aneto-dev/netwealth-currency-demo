using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetWealth.Data.Entities
{
    public class CountryCurrency
    {
        [Key]
        public Guid Id { get; set; }

        public short Reference { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string Code { get; set; }
    }
}
