//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantProccessingDependencyValidationException : Xeption
    {
        public ApplicantProccessingDependencyValidationException(Xeption innerException)
            : base(message: "Applicant proccessing dependency validation error occured, contact support",
                  innerException)
        { }
    }
}
