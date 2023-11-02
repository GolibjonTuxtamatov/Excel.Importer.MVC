//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantOrchestrationServiceException : Xeption
    {
        public ApplicantOrchestrationServiceException(Xeption innerException)
            : base(message: "Applicant orchestration service error occured, contact support",
                  innerException)
        { }
    }
}
