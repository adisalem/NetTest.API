using NetTest.Domain.Common.Seed;
using NetTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetTest.Domain.Entities
{
    public class PersonEntity:BaseEntity<Person>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? AddressId { get; set; }
        public virtual AddressEntity Address { get; set; }

        public PersonEntity()
        {
            Id = new long();
        }

        public PersonEntity(Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            Address = person.Address != null ? new AddressEntity(person.Address): null;


        }

        public override Person MapToModel()
        {
            Person person = new Person();
            person.FirstName=FirstName;
            person.LastName=LastName;
            person.Address = Address.MapToModel();

            return person;
        }
        public override Person MapToModel(Person t)
        {
            Person person = t;
            person.FirstName = FirstName;
            person.LastName = LastName;
            Address = person.Address != null ? new AddressEntity(person.Address) : null;
            return person;
        }
    }
}