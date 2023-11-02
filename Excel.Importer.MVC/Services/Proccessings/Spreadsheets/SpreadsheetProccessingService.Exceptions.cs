//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Collections.Generic;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.ExternalApplicants;
using Excel.Importer.MVC.Models.Foundations.ExternalApplicants.Exceptions;
using Excel.Importer.MVC.Services.Proccessings.Spreadsheets.Exceptions;
using Xeptions;

namespace Excel.Importer.MVC.Services.Proccessings.Spreadsheets
{
    public partial class SpreadsheetProccessingService
    {
        private delegate ValueTask<List<ExternalApplicant>> ReturningExternalApplicantFunction();

        private async ValueTask<List<ExternalApplicant>> TryCatch(ReturningExternalApplicantFunction returningExternalApplicantFunction)
        {
            try
            {
                return await returningExternalApplicantFunction();
            }
            catch (ExcelFileValidationException excelFileValidationException)
            {
                throw CreateAndLogProccessingValidationException(excelFileValidationException);
            }
        }

        private ExcelFileProccessingValidationException CreateAndLogProccessingValidationException(Xeption exception)
        {
            var excelFileProccessingValidationException =
                new ExcelFileProccessingValidationException(exception.InnerException as Xeption);

            this.loggingBroker.LogError(excelFileProccessingValidationException);

            return excelFileProccessingValidationException;
        }
    }
}
