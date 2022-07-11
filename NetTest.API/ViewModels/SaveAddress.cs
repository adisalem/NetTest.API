using NetTest.Domain.Entities;

namespace NetTest.API.ViewModels
{
    public class SaveAddress
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }

    }
    public static class AddressEntityMapper
    {
        public static AddressEntity MappToEntity(this SaveAddress model)
        {
            AddressEntity entity = new AddressEntity();
            if(model.Id!=0 )
            {
                entity.Id = model.Id;
            }
            entity.City = model.City;
            entity.AddressLine = model.AddressLine;
            return entity;
        }

    }
}