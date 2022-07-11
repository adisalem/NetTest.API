using NetTest.Domain.Common.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetTest.Domain.Models
{
    public class Person
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? AddressId { get; set; }
        public virtual Address Address { get; set; }

    }
}
