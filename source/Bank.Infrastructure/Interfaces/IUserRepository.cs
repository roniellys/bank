using Bank.Domain.Entities;
using System.Threading.Tasks;

namespace Bank.Infrastructure.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByName(string name);
        Task<List<User>> SearchByEmail(string email);
        Task<List<User>> SearchByName(string nome);
    }
}