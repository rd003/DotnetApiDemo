using DotnetApiDemo.Data.Models;

namespace DotnetApiDemo.Data.Repositories
{
    public interface IPersonRepository
    {
        Task AddPerson(Person person);
        Task DeletePerson(int id);
        Task<IEnumerable<Person>> GetPeople();
        Task<Person?> GetPersonById(int id);
        Task UpdatePerson(Person person);
    }
}