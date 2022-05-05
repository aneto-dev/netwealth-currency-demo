using System;
using System.ComponentModel.DataAnnotations;

namespace NetWealth.Data.Entities
{
    public class Currency
    { 
        [Key]
        public Guid Id { get; set; }    

        public int Reference { get; set; }

        public decimal Rate { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}
