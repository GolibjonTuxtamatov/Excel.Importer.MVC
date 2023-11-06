//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Foundations.Groups.Exceptions;
using Excel.Importer.MVC.Services.Orchestrations.Groups;
using Excel.Importer.MVC.Services.Orchestrations.Groups.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Excel.Importer.MVC.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupOrchestrationService groupOrchestrationService;

        public GroupsController(IGroupOrchestrationService groupOrchestrationService)
        {
            this.groupOrchestrationService = groupOrchestrationService;
        }

        [HttpGet]
        public IActionResult PostGroup()
        {
            return View("PostGroup");
        }

        [HttpPost]
        public async ValueTask<ActionResult<Group>> PostGroup(Group group)
        {
            try
            {
                Group postedGroup = await this.groupOrchestrationService.AddGroupAsync(group);

                return RedirectToAction("GetAllGroups");
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

        [HttpGet]
        public async ValueTask<ActionResult<Group>> Edit(Guid id)
        {
            Group group = await this.groupOrchestrationService.RetrieveGroupByIdAsync(id);

            return View(group);
        }

        [HttpPost]
        public IActionResult UpdateGroup(Group group)
        {
            this.groupOrchestrationService.UpdateGroupAsync(group);

            return RedirectToAction("GetAllGroups");
        }

        [HttpGet]
        public IActionResult DeleteGroup(Guid id)
        {
            Group group =
                this.groupOrchestrationService.RetrieveGroupByIdAsync(id).Result;

            this.groupOrchestrationService.DeleteGroupAsync(group);

            return RedirectToAction("GetAllGroups");
        }

        [HttpGet]
        public IActionResult SearchGroup(string searchString)
        {
            var groups = this.groupOrchestrationService.RetrieveAllGroups().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                groups = groups.Where(a => a.GroupName.Contains(searchString) || a.GroupName.Contains(searchString)).ToList();
            }

            return View(groups);
        }
    }
}
