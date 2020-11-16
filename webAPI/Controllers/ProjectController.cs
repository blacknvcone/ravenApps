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
    - Refactoring Into Async Task Mode [DONE]
    - Pagination
    - Search Filter
    - Sorting
    - Security (JWT)
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
        public async Task<IActionResult> GetAllProject(){
            
            var projects = await repositoryWrapper.Project.GetAllProjectsAsync();
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
        public async Task<IActionResult> GetProject(int id){
            var project = await repositoryWrapper.Project.GetProjectAsync(id);
            if(project == null){
                 loggerManager.LogInfo($"Project with id: {id} doesn't exist in the database.");
                 return NotFound();
            }else{
                return Ok(project);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]Project project){
            var _proj = new Project();
            _proj.name = project.name;
            _proj.company = project.company;
            _proj.active_start_date = project.active_start_date;
            _proj.active_end_date = project.active_end_date;
            _proj.publish = project.publish;

            repositoryWrapper.Project.CreateProject(_proj);
            await repositoryWrapper.SaveAsync();
            return CreatedAtRoute("ProjectById", new { id = _proj.id }, _proj);
        }

        [HttpDelete("{id}", Name = "DeleteProjectById")]
        public async Task<IActionResult> DeleteProject(int id){
            var _proj = await repositoryWrapper.Project.GetProjectAsync(id);
            if(_proj == null){
                 loggerManager.LogInfo($"Project with id: {id} doesn't exist in the database.");
                 return NotFound();
            }

            repositoryWrapper.Project.DeleteProject(_proj);
            await repositoryWrapper.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateProjectById")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody]ProjectUpdateDTO project){
            var _proj = await repositoryWrapper.Project.GetProjectAsync(id);
            if(_proj == null){
                 loggerManager.LogInfo($"Project with id: {id} doesn't exist in the database.");
                 return NotFound();
            }
            _mapper.Map(project, _proj);
            repositoryWrapper.Project.UpdateProject(_proj);
            await repositoryWrapper.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}", Name = "UpdateProjectValueById")]
        public async Task<IActionResult> PatchProject(int id,[FromBody]JsonPatchDocument<ProjectUpdateDTO> projectPatchDoc){
            if(projectPatchDoc == null){
                  loggerManager.LogInfo("projectPathDoc object null");
                 return BadRequest();
            }

            var _proj = await repositoryWrapper.Project.GetProjectAsync(id);
            if(_proj == null){
                 loggerManager.LogInfo($"Project with id: {id} doesn't exist in the database.");
                 return NotFound();
            }
            var projectPacthed = _mapper.Map<ProjectUpdateDTO>(_proj);
            projectPatchDoc.ApplyTo(projectPacthed);
            _mapper.Map(projectPacthed, _proj);
            repositoryWrapper.Project.UpdateProject(_proj);
            await repositoryWrapper.SaveAsync();

            return NoContent();
        }
    }
}