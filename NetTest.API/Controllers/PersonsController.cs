using Microsoft.AspNetCore.Mvc;
using NetTest.API.Utilities;
using NetTest.API.ViewModels;
using NetTest.Domain.InterFaces;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }
       
        [HttpPost("Save")]
        public async Task<ActionResult<long>> Save(string json)
        {
            SavePerson person = new SavePerson();
            SaveAddress address = new SaveAddress();
            try {
                person = CustomSerializerDeserializer.DeSerialize<SavePerson>(json, person);
                address = CustomSerializerDeserializer.DeSerialize<SaveAddress>(json, address);
                person.Address = address;
                if(person.Id==0)
                {
                   return Ok(await _personService.Add(person.MappToEntity()));
                }
                else
                {
                    return Ok(await _personService.UpdatePerson(person.MappToEntity()));
                }
            }
            catch
            {
               return BadRequest("Bad Request!");
            }

        }

        [HttpGet("GetAll")]
        public async Task<string> GetAll([FromQuery] GetAllRequest request)
        {
            StringBuilder persons =new StringBuilder();
            try
            {
                persons.Append("["+" ");
                var filteredPersons = await _personService.GetAllFiltered(request.FirstName, request.LastName, request.City);
                foreach (var p in filteredPersons)
                {
                    StringBuilder sb = new StringBuilder();
                    //var personDto = new
                    //{
                    //    FirstName = p.FirstName,
                    //    LastName = p.LastName,
                    //    Address = new
                    //    {
                    //        City = p.Address.City,
                    //        AddressLine = p.Address.AddressLine
                    //    }
                    //};
                    persons.Append(CustomSerializerDeserializer.Serialize(p, sb).Trim());
                    persons.Append(",");
                }
                persons.Remove(persons.Length - 1,1);
                persons.Append(" "+"]");
                return persons.ToString();
            }
            catch
            {
                persons.Clear();
                return persons.Append("Bad Request").ToString();
            }
        }
    }
}
