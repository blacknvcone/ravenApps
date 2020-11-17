using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.RequestFeatures;

namespace Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}

        public async Task<IEnumerable<Project>> GetAllProjectsAsync(ProjectParameters projectParameters){
            return await FindAll().OrderBy(ox => ox.name)
            .Skip((projectParameters.PageNumber - 1) * projectParameters.PageSize)
            .Take(projectParameters.PageSize)
            .ToListAsync();
        }

        public async Task<Project> GetProjectAsync(int projId){
            return await FindByCondition(c => c.id.Equals(projId)).FirstOrDefaultAsync();
        }

        public void CreateProject(Project proj){
            Create(proj);
        }
        public void DeleteProject(Project proj){
            Delete(proj);
        }

        public void UpdateProject(Project proj){
           Update(proj);
        }

    }
}