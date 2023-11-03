//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Foundations.Groups.Exceptions;
using Excel.Importer.MVC.Services.Orchestrations.Groups;
using Excel.Importer.MVC.Services.Orchestrations.Groups.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Excel.Importer.MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : Controller
    {
        private readonly IGroupOrchestrationService groupOrchestrationService;

        public GroupsController(IGroupOrchestrationService groupOrchestrationService)
        {
            this.groupOrchestrationService = groupOrchestrationService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Group>> PostGroupAsync(Group group)
        {
            try
            {
                Group postedGroup = await this.groupOrchestrationService.AddGroupAsync(group);

                return View(postedGroup);
            }
            catch (GroupOrchestrationValidationException groupOrchestrationValidationException)
            {
                return BadRequest(groupOrchestrationValidationException.InnerException);
            }
            catch (GroupOrchestratioDependencyValidationException groupOrchestrationDependencyvalidationExecption)
                when (groupOrchestrationDependencyvalidationExecption.InnerException is AlreadyExistGroupException)
            {
                return Conflict(groupOrchestrationDependencyvalidationExecption.InnerException);
            }
            catch (GroupOrchestratioDependencyValidationException groupOrchestrationDependencyvalidationExecption)
            {
                return Conflict(groupOrchestrationDependencyvalidationExecption.InnerException);
            }
            catch (GroupOrchestrationDependencyException groupOrchestrationDependencyException)
            {
                return BadRequest(groupOrchestrationDependencyException.InnerException);
            }
            catch (GroupOrchestrationServiceException groupOrchestrationServiceException)
            {
                return BadRequest(groupOrchestrationServiceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Group>> GetAllGroups()
        {
            try
            {
                IQueryable<Group> groups = this.groupOrchestrationService.RetrieveAllGroups();

                return View(groups);
            }
            catch (GroupOrchestrationDependencyException groupOrchestrationDependencyException)
            {
                return BadRequest(groupOrchestrationDependencyException.InnerException);
            }
            catch (GroupOrchestrationServiceException groupOrchestrationServiceException)
            {
                return BadRequest(groupOrchestrationServiceException);
            }
        }
    }
}
