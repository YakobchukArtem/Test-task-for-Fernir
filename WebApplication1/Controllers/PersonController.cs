using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;
using WebApplication1.Repositories.Interface;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        //https://localhost:45100/api/Person
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddPersonDto request)
        {
            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            await personRepository.CreateAsync(person);
            var response = new PersonDto
            {
                id = person.id,
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
            return Ok(response);
        }
        //https://localhost:45100/api/Person
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var people = await personRepository.GetAllAsync();
            var response = new List<PersonDto>();
            foreach (var person in people)
            {
                response.Add(new PersonDto
                {
                    id = person.id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                });
            }
            return Ok(response);
        }
        //https://localhost:45100/api/Person/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await personRepository.DeleteAsync(id);
            return Ok();
        }
        //https://localhost:45100/api/Person/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddPersonDto request)
        {
            var existingPerson = await personRepository.GetByIdAsync(id);

            if (existingPerson == null)
            {
                return NotFound();
            }

            existingPerson.FirstName = request.FirstName;
            existingPerson.LastName = request.LastName;

            await personRepository.UpdateAsync(id, existingPerson);

            var response = new PersonDto
            {
                id = existingPerson.id,
                FirstName = existingPerson.FirstName,
                LastName = existingPerson.LastName,
            };

            return Ok(response);
        }


    }
}
