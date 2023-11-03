//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Services.Orchestrations.Spreadsheets;
using Excel.Importer.MVC.Services.Orchestrations.Spreadsheets.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Excel.Importer.MVC.Controllers
{
    public class ExcelsController : Controller
    {
        private readonly ISpreadsheetOrchestrationService spreadsheetOrchestrationService;

        public ExcelsController(ISpreadsheetOrchestrationService spreadsheetOrchestrationService)
        {
            this.spreadsheetOrchestrationService = spreadsheetOrchestrationService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<ICollection<Applicant>>> ImportExternalApplicant(IFormFile formFile)
        {
            try
            {
                if (formFile == null)
                    return BadRequest("File is null");

                using var memoryStream = new MemoryStream();
                formFile.CopyTo(memoryStream);

                ICollection<Applicant> posetdApplicants =
                    await this.spreadsheetOrchestrationService.ImportExternalApplicants(memoryStream);

                return View(posetdApplicants);
            }
            catch (ExcelFileOrchestrationValidationException excelFileOrchestrationValdationException)
            {
                return BadRequest(excelFileOrchestrationValdationException.InnerException);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
