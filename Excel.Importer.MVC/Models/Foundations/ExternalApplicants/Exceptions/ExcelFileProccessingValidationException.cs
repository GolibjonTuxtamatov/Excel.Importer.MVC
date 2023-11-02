//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Proccessings.Spreadsheets.Exceptions
{
    public class ExcelFileProccessingValidationException : Xeption
    {
        public ExcelFileProccessingValidationException(Xeption innerException)
            : base("Excel proccessing validation error occured, fix the error and try again",
                 innerException)
        { }
    }
}
