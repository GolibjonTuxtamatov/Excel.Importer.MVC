//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Brokers.Storages;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Orchestrations.Applicants;
using Excel.Importer.MVC.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Excel.Importer.MVC.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly IApplicantOrchestrationService applicantOrchestrationService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ApplicantsController(IApplicantOrchestrationService applicantOrchestrationService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.applicantOrchestrationService = applicantOrchestrationService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult PostApplicant()
        {
            return View("PostApplicant");
        }

        [HttpPost]
        public async ValueTask<ActionResult<Applicant>> PostApplicant(Applicant applicant)
        {
            try
            {
                Applicant postedApplicant =
                    await this.applicantOrchestrationService.AddApplicantAsync(applicant);

                return RedirectToAction("GetAllApplicants");
            }
            catch (ApplicantOrchestrationValidationException applicantOrchestrationValidationException)
            {
                return BadRequest(applicantOrchestrationValidationException.InnerException);
            }
            catch (ApplicantOrchetrationDependencyValidationException applicantOrchetrationDependencyValidationException)
                when (applicantOrchetrationDependencyValidationException.InnerException is AlreadyExistApplicantException)
            {
                return Conflict(applicantOrchetrationDependencyValidationException.InnerException);
            }
            catch (ApplicantOrchetrationDependencyValidationException applicantOrchetrationDependencyValidationException)
            {
                return Conflict(applicantOrchetrationDependencyValidationException.InnerException);
            }
            catch (ApplicantOrchestrationDependencyException applicantOrchestrationDependencyException)
            {
                return BadRequest(applicantOrchestrationDependencyException.InnerException);
            }
            catch (ApplicantOrchestrationServiceException applicantOrchestrationServiceException)
            {
                return BadRequest(applicantOrchestrationServiceException.InnerException);
            }
        }

        [HttpGet]
        public IActionResult PostApplicantInGroup(Guid id)
        {
            Applicant applicant =
                this.applicantOrchestrationService.RetrieveApplicantByIdAsync(id).Result;

            applicant.FirstName = string.Empty;
            applicant.LastName = string.Empty;
            applicant.Email = string.Empty;
            applicant.PhoneNumber = string.Empty;

            return View(applicant);
        }

        [HttpPost]
        public IActionResult PostApplicantInGroup(Applicant applicant)
        {
            Applicant postedApplicant =
                this.applicantOrchestrationService.AddApplicantInGroup(applicant).Result;

            List<Applicant> applicants =
                this.applicantOrchestrationService.RetrieveAllApplicants()
                .Where(findingApplicant => findingApplicant.GroupId == applicant.GroupId).ToList();

            return View("GetApplicantsByGroupName", applicants);
        }

        [HttpGet]
        public ActionResult<IQueryable<Applicant>> GetAllApplicants()
        {
            IQueryable<Applicant> applicants = this.applicantOrchestrationService.RetrieveAllApplicants();

            return View(applicants);
        }

        [HttpGet]
        public ActionResult<IQueryable<Applicant>> GetApplicantsByGroupName(Guid id)
        {
            List<Applicant> applicants =
                this.applicantOrchestrationService.RetrieveAllApplicants().ToList();

            List<Applicant> applicantWithGroup =
                applicants.Where(applicant => applicant.GroupId == id).ToList();



            return View(applicantWithGroup);
        }

        [HttpGet]
        public async ValueTask<IActionResult> EditApplicant(Guid id)
        {
            Applicant applicant = await this.applicantOrchestrationService.RetrieveApplicantByIdAsync(id);

            return View(applicant);
        }

        [HttpPost]
        public async ValueTask<IActionResult> UpdateApplicant(Applicant applicant)
        {
            await this.applicantOrchestrationService.ModifyApplicantAsync(applicant);

            return RedirectToAction("GetAllApplicants");
        }

        [HttpGet]
        public IActionResult DeleteApplicant(Guid id)
        {
            Applicant applicant =
                this.applicantOrchestrationService.RetrieveApplicantByIdAsync(id).Result;

            this.applicantOrchestrationService.RemoveApplicantAsync(applicant);

            return RedirectToAction("GetAllApplicants");
        }

        [HttpGet]
        public IActionResult SearchApplicant(string searchString)
        {
            var applicants = this.applicantOrchestrationService.RetrieveAllApplicants().ToList();

            List<Applicant> foundApplicants = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                foundApplicants = applicants.Where(a =>
                    a.FirstName.ToLower() == searchString.ToLower() ||
                    a.LastName.ToLower() == searchString.ToLower() ||
                    a.GroupName.ToLower() == searchString.ToLower()).ToList();
            }

            return View(foundApplicants);
        }

        [HttpGet]
        public IActionResult SearchApplicantInGroup(string searchString, Guid id)
        {
            var applicants = this.applicantOrchestrationService.RetrieveAllApplicants().ToList();

            List<Applicant> applicantWithGroup =
                applicants.Where(applicant => applicant.GroupId == id).ToList();

            List<Applicant> foundApplicants = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                foundApplicants = applicantWithGroup.Where(a =>
                    a.FirstName.ToLower() == searchString.ToLower() ||
                    a.LastName.ToLower() == searchString.ToLower() ||
                    a.GroupName.ToLower() == searchString.ToLower()).ToList();
            }

            return View(foundApplicants);
        }


        [HttpGet]
        public IActionResult DownloadExcel(Guid id)
        {

            string fileName = this.applicantOrchestrationService.DownloadExcel(id);

            string filePath =
                Path.Combine(webHostEnvironment.WebRootPath, "spreadsheets", fileName);

            byte[] excelFile = System.IO.File.ReadAllBytes(filePath);

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(excelFile, contentType, fileName);
        }
    }
}