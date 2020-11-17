using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync(ProjectParameters projectParameters);
        Task<Project> GetProjectAsync(int id);

        void CreateProject(Project project);
        void DeleteProject(Project project);
        void UpdateProject(Project project);
    }


    
}