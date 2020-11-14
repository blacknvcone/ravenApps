using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAllProjects();
        Project GetProject(int id);

        void CreateProject(Project project);
    }


    
}