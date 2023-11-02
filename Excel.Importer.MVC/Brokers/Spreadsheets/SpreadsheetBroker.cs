//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.ExternalApplicants;
using OfficeOpenXml;

namespace Excel.Importer.MVC.Brokers.Spreadsheets
{
    public class SpreadsheetBroker : ISpreadsheetBroker
    {
        public async ValueTask<List<ExternalApplicant>> ReadSpreadsheetAsync(MemoryStream spreadsheet)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var excelPackage = new ExcelPackage(spreadsheet);

            var externalApplicants = new List<ExternalApplicant>();

            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

            for (int row = 2; row < worksheet.Dimension.Rows + 1; row++)
            {
                externalApplicants.Add(
                    new ExternalApplicant
                    {
                        FirstName = worksheet.Cells[row, 1].Value.ToString(),
                        LastName = worksheet.Cells[row, 2].Value.ToString(),
                        Email = worksheet.Cells[row, 3].Value.ToString(),
                        PhoneNumber = worksheet.Cells[row, 4].Value.ToString(),
                        GroupName = worksheet.Cells[row, 5].Value.ToString()
                    });
            }

            return externalApplicants;
        }
    }
}
