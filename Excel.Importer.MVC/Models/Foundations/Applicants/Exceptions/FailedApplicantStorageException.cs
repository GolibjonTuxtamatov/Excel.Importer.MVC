//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class FailedApplicantStorageException : Xeption
    {
        public FailedApplicantStorageException(Exception innerException)
            : base(message: "Failed applicant storage error occured, contact support",
                  innerException)
        { }
    }
}
