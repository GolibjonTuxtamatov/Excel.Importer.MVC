﻿//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions;
using Excel.Importer.MVC.Services.Orchestrations.Applicants;
using Microsoft.AspNetCore.Mvc;

namespace Excel.Importer.MVC.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly IApplicantOrchestrationService applicantOrchestrationService;

        public ApplicantsController(IApplicantOrchestrationService applicantOrchestrationService)
        {
            this.applicantOrchestrationService = applicantOrchestrationService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Applicant>> PostApplicantAsync(Applicant applicant)
        {
            try
            {
                Applicant postedApplicant = await this.applicantOrchestrationService.AddApplicantAsync(applicant);

                return postedApplicant;
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
    }
}