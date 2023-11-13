//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Excel.Importer.MVC.Brokers.Storages
{
    public partial class StorageBroker
    {
        
        public string DownloadExcel(Guid id)
        {
            using var broker = new StorageBroker(this.configuration, webHostEnvironment);

            IQueryable<Applicant> applicants = broker.Applicants.Where(applicant =>
                applicant.GroupId == id);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            string fileName = Guid.NewGuid().ToString();

            using (var package = new ExcelPackage())
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets.Add(fileName);

                excelWorksheet.Cells[1, 1].Value = "FirstName";
                excelWorksheet.Cells[1, 2].Value = "LastName";
                excelWorksheet.Cells[1, 3].Value = "Email";
                excelWorksheet.Cells[1, 4].Value = "Phone";
                excelWorksheet.Cells[1, 5].Value = "Group";

                int row = 2;

                foreach (Applicant applicant in applicants)
                {
                    excelWorksheet.Cells[row, 1].Value = applicant.FirstName;
                    excelWorksheet.Cells[row, 2].Value = applicant.LastName;
                    excelWorksheet.Cells[row, 3].Value = applicant.Email;
                    excelWorksheet.Cells[row, 4].Value = applicant.PhoneNumber;
                    excelWorksheet.Cells[row, 5].Value = applicant.GroupName;
                    row++;
                }

                string rootPath = webHostEnvironment.WebRootPath;

                string filePath = Path.Combine(rootPath, "spreadsheets", fileName+".xlsx");

                package.SaveAs(new FileInfo(filePath));
            }

            return fileName+".xlsx";
        }
    }
}
