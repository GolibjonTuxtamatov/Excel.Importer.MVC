//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantProccessingValidationException : Xeption
    {
        public ApplicantProccessingValidationException(Xeption innerException)
            : base(message: "Applicant proccessing service error occured, contact support",
                  innerException)
        { }
    }
}
