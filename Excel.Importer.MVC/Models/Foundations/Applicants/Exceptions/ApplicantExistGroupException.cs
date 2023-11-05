//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions
{
    public class ApplicantExistGroupException :Xeption
    {
        public ApplicantExistGroupException()
            :base("Group is not found")
        {}
    }
}
