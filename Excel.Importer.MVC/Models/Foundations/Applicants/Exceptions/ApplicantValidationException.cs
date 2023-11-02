//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantValidationException : Xeption
    {
        public ApplicantValidationException(Xeption innerException)
            : base(message: "Applicant Validation error occurred, fix the errors and and try again.",
                  innerException)
        { }
    }
}
