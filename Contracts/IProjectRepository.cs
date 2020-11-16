using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectAsync(int id);

        void CreateProject(Project project);
        void DeleteProject(Project project);
        void UpdateProject(Project project);
    }


    
}