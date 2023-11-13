//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;

namespace Excel.Importer.MVC.Services.Foundations.Applicants
{
    public interface IApplicantService
    {
        ValueTask<Applicant> AddApplicantAsync(Applicant applicant);
        IQueryable<Applicant> RetrieveAllApplicant();
        ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid Id);
        ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant);
        ValueTask<Applicant> RemoveApplicantAsync(Applicant applicant);

        string DownloadExcel(Guid id);
    }
}
