//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.ExternalApplicants;
using Excel.Importer.MVC.Models.Foundations.ExternalApplicants.Exceptions;
using Xeptions;

namespace Excel.Importer.MVC.Services.Foundations.Spreadsheets
{
    public partial class SpreadsheetService
    {
        private delegate ValueTask<List<ExternalApplicant>> ReturningExternalApplicantFunction();

        private async ValueTask<List<ExternalApplicant>> TryCatch(ReturningExternalApplicantFunction returningExternalApplicantFunction)
        {
            try
            {
                return await returningExternalApplicantFunction();
            }
            catch (InvalidDataException invalidDataException)
            {
                var invalidExcelFileException =
                    new InvalidExcelFileException(invalidDataException);

                throw CreateAndLogValidationException(invalidExcelFileException);
            }
            catch (NullReferenceException nullReferenceException)
            {
                var nullExcelFileException =
                    new NullExcelFileException(nullReferenceException);

                throw CreateAndLogValidationException(nullExcelFileException);
            }
        }

        private ExcelFileValidationException CreateAndLogValidationException(Xeption exception)
        {
            var excelFileValidationException =
                    new ExcelFileValidationException(exception);

            this.loggingBroker.LogError(excelFileValidationException);

            return excelFileValidationException;
        }
    }
}
