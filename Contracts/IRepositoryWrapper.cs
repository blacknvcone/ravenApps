using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryWrapper 
    { 
        IProjectRepository Project {get;}
        void Save(); 
    }
}
