using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contracts;

namespace webAPI.Controllers{
    
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase{
        private IRepositoryWrapper repositoryWrapper;
        public ProjectController(IRepositoryWrapper repository){
            repositoryWrapper = repository;
        }

        [HttpGet]
        public IActionResult getAllProjects(){
            {
            try
            {
                var projects = repositoryWrapper.Project.GetAllProjects();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        }
    }
}