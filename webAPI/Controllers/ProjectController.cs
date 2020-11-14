using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Contracts;
using Entities;
using Entities.Models;

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
        public IActionResult GetAllProject(){
            
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

        [HttpGet("{id}")]
        public IActionResult GetProject(int id){
            var project = repositoryWrapper.Project.GetProject(id);
            if(project == null){
                 loggerManager.LogInfo($"Project with id: {id} doesn't exist in the database.");
                 return NotFound();
            }else{
                return Ok(project);
            }

        }

        [HttpPost]
        public IActionResult CreateProject([FromBody]Project project){
            var _proj = new Project();
            _proj.name = project.name;
            _proj.company = project.company;
            _proj.active_start_date = project.active_start_date;
            _proj.active_end_date = project.active_end_date;
            _proj.publish = project.publish;

            repositoryWrapper.Project.CreateProject(_proj);
            repositoryWrapper.Save();
            return Ok(_proj);
        }
    }
}