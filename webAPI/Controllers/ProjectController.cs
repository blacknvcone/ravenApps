using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using Contracts;
using Entities;
using Entities.Models;
using AutoMapper;

/*
TODO : 
    - Refactoring Into Async Task Mode
*/
namespace webAPI.Controllers{
    
    [ApiController]
    [Route("api/project")]
    public class ProjectController : ControllerBase{
        private IRepositoryWrapper repositoryWrapper;
        private ILoggerManager loggerManager;
        private IMapper _mapper;

        public ProjectController(IRepositoryWrapper repository, ILoggerManager logger, IMapper mapper){
            loggerManager = logger;
            repositoryWrapper = repository;
            _mapper = mapper;
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

        [HttpGet("{id}",  Name = "ProjectById")]
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
            return CreatedAtRoute("ProjectById", new { id = _proj.id }, _proj);
        }

        [HttpDelete("{id}", Name = "DeleteProjectById")]
        public IActionResult DeleteProject(int id){
            var _proj = repositoryWrapper.Project.GetProject(id);
            if(_proj == null){
                 loggerManager.LogInfo($"Project with id: {id} doesn't exist in the database.");
                 return NotFound();
            }

            repositoryWrapper.Project.DeleteProject(_proj);
            repositoryWrapper.Save();
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateProjectById")]
        public IActionResult UpdateProject(int id, [FromBody]ProjectUpdateDTO project){
            var _proj = repositoryWrapper.Project.GetProject(id);
            if(_proj == null){
                 loggerManager.LogInfo($"Project with id: {id} doesn't exist in the database.");
                 return NotFound();
            }
            _mapper.Map(project, _proj);
            repositoryWrapper.Project.UpdateProject(_proj);
            repositoryWrapper.Save();

            return NoContent();
        }

        [HttpPatch("{id}", Name = "UpdateProjectValueById")]
        public IActionResult PatchProject(int id,[FromBody]JsonPatchDocument<ProjectUpdateDTO> projectPatchDoc){
            if(projectPatchDoc == null){
                  loggerManager.LogInfo("projectPathDoc object null");
                 return BadRequest();
            }

            var _proj = repositoryWrapper.Project.GetProject(id);
            if(_proj == null){
                 loggerManager.LogInfo($"Project with id: {id} doesn't exist in the database.");
                 return NotFound();
            }
            var projectPacthed = _mapper.Map<ProjectUpdateDTO>(_proj);
            projectPatchDoc.ApplyTo(projectPacthed);
            _mapper.Map(projectPacthed, _proj);
            repositoryWrapper.Project.UpdateProject(_proj);
            repositoryWrapper.Save();

            return NoContent();
        }
    }
}