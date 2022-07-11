using NetTest.Domain.Entities;

namespace NetTest.API.ViewModels
{
    public class SavePerson
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SaveAddress Address { get; set; }
    }
    public static class PersonEntityMapper
    {
        public static PersonEntity MappToEntity(this SavePerson model)
        {
            PersonEntity entity = new PersonEntity();
            if (model.Id != 0)
            {
                entity.Id = model.Id;
            }
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Address = model.Address.MappToEntity();
            return entity;
        }

    }
}