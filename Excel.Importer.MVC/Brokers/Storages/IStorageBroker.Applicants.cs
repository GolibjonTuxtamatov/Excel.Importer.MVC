//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;

namespace Excel.Importer.MVC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Applicant> InsertApplicantAsync(Applicant applicant);
        IQueryable<Applicant> SelectAllApplicant();
        ValueTask<Applicant> SelectAllApplicantsByIdAsync(Guid Id);
        ValueTask<Applicant> UpdateApplicantAsync(Applicant applicant);
        ValueTask<Applicant> DeleteApplicantAsync(Applicant applicant);
    }
}
