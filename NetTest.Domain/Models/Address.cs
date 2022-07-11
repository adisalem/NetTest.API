using NetTest.Domain.Common.Seed;
using System.ComponentModel.DataAnnotations;

namespace NetTest.Domain.Models
{
    public class Address
    {
        [Key]
        public long Id { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
    }
}
