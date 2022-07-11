using NetTest.Domain.Common.Seed;
using NetTest.Domain.Models;

namespace NetTest.Domain.Entities
{
    public class AddressEntity:BaseEntity<Address>
    {
        public string City { get; set; }
        public string AddressLine { get; set; }
        public AddressEntity()
        {
        }

        public AddressEntity(Address address)
        {
            City = address.City;
            AddressLine = address.AddressLine;

        }

        public override Address MapToModel()
        {
            Address address = new Address();
            address.City = City;
            address.AddressLine = AddressLine;
            return address;
        }
        public override Address MapToModel(Address t)
        {
            Address address = t;
            address.City = City;
            address.AddressLine = AddressLine;
            return address;
        }
    }
}