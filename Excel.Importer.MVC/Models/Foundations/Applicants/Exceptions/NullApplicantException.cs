//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class NullApplicantException : Xeption
    {
        public NullApplicantException()
            : base(message: "Applicant is null")
        { }
    }
}
