//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Microsoft.EntityFrameworkCore;

namespace Excel.Importer.MVC.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Applicant> Applicants { get; set; }

        public async ValueTask<Applicant> InsertApplicantAsync(Applicant applicant) =>
            await InsertAsync(applicant);

        public IQueryable<Applicant> SelectAllApplicant() =>
            SelectAll<Applicant>();

        public async ValueTask<Applicant> SelectAllApplicantsByIdAsync(Guid Id) =>
            await SelectAsync<Applicant>(Id);

        public async ValueTask<Applicant> UpdateApplicantAsync(Applicant applicant) =>
            await UpdateAsync(applicant);

        public async ValueTask<Applicant> DeleteApplicantAsync(Applicant applicant) =>
            await DeleteAsync(applicant);
    }
}
