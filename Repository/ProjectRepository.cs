using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}

        public async Task<IEnumerable<Project>> GetAllProjectsAsync(){
            return await FindAll().OrderBy(ox => ox.name).ToListAsync();
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