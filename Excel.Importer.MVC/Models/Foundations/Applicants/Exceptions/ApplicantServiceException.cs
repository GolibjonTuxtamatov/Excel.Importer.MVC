//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantServiceException : Xeption
    {
        public ApplicantServiceException(Xeption innerException)
            : base(message: "Applicant service error occured, contact support", innerException)
        { }
    }
}
