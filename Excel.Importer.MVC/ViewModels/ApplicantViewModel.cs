//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Collections;
using System.Collections.Generic;
using Excel.Importer.MVC.Models.Foundations.Applicants;

namespace Excel.Importer.MVC.ViewModels
{
    public class ApplicantViewModel
    {
        public List<Applicant> Applicants { get; set; }
        public Applicant Applicant { get; set; }
    }
}
