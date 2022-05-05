using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetWealth.Data.Entities
{
    public class Country
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
