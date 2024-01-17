using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Repositories.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext dbContext;
        public PersonRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Person> CreateAsync(Person person)
        {
            await dbContext.Person.AddAsync(person);
            await dbContext.SaveChangesAsync();
            return person;
        }

        public async Task DeleteAsync(Guid personId)
        {
            var personToDelete = await dbContext.Person.FindAsync(personId);
            if (personToDelete != null)
            {
                dbContext.Person.Remove(personToDelete);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(Guid personId, Person updatedPerson)
        {
            var existingPerson = await dbContext.Person.FirstOrDefaultAsync(p => p.id == personId);

            if (existingPerson != null)
            {
                existingPerson.FirstName = updatedPerson.FirstName;
                existingPerson.LastName = updatedPerson.LastName;

                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await dbContext.Person.ToListAsync();
        }
        public async Task<Person> GetByIdAsync(Guid id)
        {
            return await dbContext.Person.FirstOrDefaultAsync(c => c.id == id);
        }
    }
}
