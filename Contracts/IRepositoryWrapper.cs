using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper 
    { 
        IProjectRepository Project {get;}
        Task SaveAsync(); 
    }
}
