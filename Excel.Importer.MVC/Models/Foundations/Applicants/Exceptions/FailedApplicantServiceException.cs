//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class FailedApplicantServiceException : Xeption
    {
        public FailedApplicantServiceException(Exception innerException)
            : base(message: "failed applicant service error occured, contact support",
                  innerException)
        { }
    }
}
