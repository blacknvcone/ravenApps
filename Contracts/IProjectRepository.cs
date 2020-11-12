using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IProjectRepository : IRepositoryBase<Project>
    {
        IEnumerable<Project> GetAllProjects();
    }


    
}