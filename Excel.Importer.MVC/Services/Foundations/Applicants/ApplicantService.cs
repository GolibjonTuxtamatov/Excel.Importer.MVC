//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Brokers.Loggings;
using Excel.Importer.MVC.Brokers.Storages;
using Excel.Importer.MVC.Models.Foundations.Applicants;

namespace Excel.Importer.MVC.Services.Foundations.Applicants
{
    public partial class ApplicantService : IApplicantService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public ApplicantService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Applicant> AddApplicantAsync(Applicant applicant) =>
        TryCatch(async () =>
        {
            ValidateApplicantOnAdd(applicant);

            return await this.storageBroker.InsertApplicantAsync(applicant);
        });

        public IQueryable<Applicant> RetrieveAllApplicant() =>
            this.storageBroker.SelectAllApplicant();
        public ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid Id) =>
            this.storageBroker.SelectApplicantByIdAsync(Id);
        public ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant) =>
            this.storageBroker.UpdateApplicantAsync(applicant);

        public async ValueTask<Applicant> RemoveApplicantAsync(Guid guid)
        {
            var selectedApplicant = await this.storageBroker.SelectApplicantByIdAsync(guid);

            return await this.storageBroker.DeleteApplicantAsync(selectedApplicant);
        }
    }
}
