//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;

namespace Excel.Importer.MVC.Services.Orchestrations.Applicants
{
    public interface IApplicantOrchestrationService
    {
        ValueTask<Applicant> AddApplicantAsync(Applicant applicant);
        IQueryable<Applicant> RetrieveAllApplicants();
        ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid Id);
        ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant);
        ValueTask<Applicant> RemoveApplicantAsync(Applicant applicant);
    }
}
