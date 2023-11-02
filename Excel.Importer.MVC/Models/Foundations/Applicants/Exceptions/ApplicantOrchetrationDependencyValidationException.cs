//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantOrchetrationDependencyValidationException : Xeption
    {
        public ApplicantOrchetrationDependencyValidationException(Xeption innerException)
            : base(message: "Applicant orchestration dependency validation error occured, fix the error and try again",
                  innerException)
        { }
    }
}
