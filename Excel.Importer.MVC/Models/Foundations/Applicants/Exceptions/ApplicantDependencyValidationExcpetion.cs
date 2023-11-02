//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantDependencyValidationExcpetion : Xeption
    {
        public ApplicantDependencyValidationExcpetion(Xeption innerException)
            : base(message: "Applicant dependency validation error occured, fix the errors and try again", innerException)
        { }
    }
}
