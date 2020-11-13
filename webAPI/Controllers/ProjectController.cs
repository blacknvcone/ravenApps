using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contracts;
using Entities;

namespace webAPI.Controllers{
    
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase{
        private IRepositoryWrapper repositoryWrapper;
        private ILoggerManager loggerManager;
        public ProjectController(IRepositoryWrapper repository, ILoggerManager logger){
            loggerManager = logger;
            repositoryWrapper = repository;
        }

        [HttpGet]
        public IActionResult getAllProjects(){
           
            var projects = repositoryWrapper.Project.GetAllProjects();
            var projDTO = projects.Select(c => new ProjectDTO{
                    idNum = c.id,
                    nama = c.name,
                    perusahaan = c.company,
                    tahunAktif = string.Join('-', c.active_start_date, c.active_end_date) 
                }).ToList();
                //throw new Exception("Testing Built in Custom Middleware Exception.");
            return Ok(projDTO);
        }
    }
}