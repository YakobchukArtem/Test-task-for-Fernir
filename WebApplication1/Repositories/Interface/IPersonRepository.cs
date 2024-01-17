
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories.Interface
{
    public interface IPersonRepository
    {
        Task<Person> CreateAsync(Person category);
        Task<IEnumerable<Person>> GetAllAsync();
        Task DeleteAsync(Guid personId);
        Task UpdateAsync(Guid personId, Person updatedPerson);
        Task<Person> GetByIdAsync(Guid id);
    }
}
