//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Orchestrations.Spreadsheets.Exceptions
{
    public class ExcelFileOrchestrationValidationException : Xeption
    {
        public ExcelFileOrchestrationValidationException(Xeption innerException)
            : base("Excel file orchestration validation error occured, fix the error and try again",
                 innerException)
        { }
    }
}
