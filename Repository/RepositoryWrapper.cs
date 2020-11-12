using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;

        private IProjectRepository _project;

        public IProjectRepository Project {
            get {
                if(_project == null)
                {
                    _project = new ProjectRepository(_repoContext);
                }

                return _project;
            }
        }


        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}