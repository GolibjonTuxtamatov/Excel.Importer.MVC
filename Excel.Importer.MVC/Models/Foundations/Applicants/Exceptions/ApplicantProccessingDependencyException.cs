//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantProccessingDependencyException : Xeption
    {
        public ApplicantProccessingDependencyException(Xeption innerException)
            : base(message: "Applicant proccessing dependency error occured, fix the error try again",
                  innerException)
        { }
    }
}
