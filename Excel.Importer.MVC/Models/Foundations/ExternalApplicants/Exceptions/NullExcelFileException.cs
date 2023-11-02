//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.ExternalApplicants.Exceptions
{
    public class NullExcelFileException : Xeption
    {
        public NullExcelFileException(Exception innerException)
            : base("Excel file some properties are null",
                 innerException)
        { }
    }
}
