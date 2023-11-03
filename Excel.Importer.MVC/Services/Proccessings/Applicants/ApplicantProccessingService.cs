//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Brokers.Loggings;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Services.Foundations.Applicants;

namespace Excel.Importer.MVC.Services.Proccessings.Applicants
{
    public partial class ApplicantProccessingService : IApplicantProccessingService
    {
        private readonly IApplicantService applicantService;
        private readonly ILoggingBroker loggingBroker;
        public ApplicantProccessingService(IApplicantService applicantService, ILoggingBroker loggingBroker)
        {
            this.applicantService = applicantService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Applicant> AddApplicantAsync(Applicant applicant) =>
            TryCatch(async () =>
        {
            return await this.applicantService.AddApplicantAsync(applicant);
        });

        public IQueryable<Applicant> RetrieveAllApplicants() =>
            this.applicantService.RetrieveAllApplicant();

        public ValueTask<Applicant> RetriveApplicantByIdAsync(Guid Id) =>
            this.applicantService.RetrieveApplicantByIdAsync(Id);

        public ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant) =>
            this.applicantService.ModifyApplicantAsync(applicant);

        public ValueTask<Applicant> RemoveApplicantAsync(Guid guid) =>
            this.RetriveApplicantByIdAsync(guid);
    }
}
