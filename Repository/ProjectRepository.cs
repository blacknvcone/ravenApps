using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}

        public IEnumerable<Project> GetAllProjects(){
            return FindAll().OrderBy(ox => ox.name).ToList();
        }

        public Project GetProject(int projId){
            return FindByCondition(c => c.id.Equals(projId)).SingleOrDefault();
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