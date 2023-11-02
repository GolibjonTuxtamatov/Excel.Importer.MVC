//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;

namespace Excel.Importer.MVC.Services.Orchestrations.Spreadsheets
{
    public interface ISpreadsheetOrchestrationService
    {
        ValueTask<ICollection<Applicant>> ImportExternalApplicants(MemoryStream spreadsheet);
    }
}
