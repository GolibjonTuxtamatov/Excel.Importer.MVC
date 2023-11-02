//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.ExternalApplicants;

namespace Excel.Importer.MVC.Services.Foundations.Spreadsheets
{
    public interface ISpreadsheetService
    {
        ValueTask<List<ExternalApplicant>> ImportExternalApplicantAsync(MemoryStream spreasheet);
    }
}
