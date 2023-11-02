//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantOrchestrationDependencyException : Xeption
    {
        public ApplicantOrchestrationDependencyException(Xeption innerException)
            : base(message: "Applicant orchestration dependency error occured, contact support",
                  innerException)
        { }
    }
}
