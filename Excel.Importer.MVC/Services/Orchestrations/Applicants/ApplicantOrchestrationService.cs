//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Brokers.Loggings;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Services.Proccessings.Applicants;

namespace Excel.Importer.MVC.Services.Orchestrations.Applicants
{
    public partial class ApplicantOrchestrationService : IApplicantOrchestrationService
    {
        private readonly IApplicantProccessingService applicantProccessingService;
        private readonly ILoggingBroker loggingBroker;

        public ApplicantOrchestrationService(
            IApplicantProccessingService applicantProccessingService,
            ILoggingBroker loggingBroker)
        {
            this.applicantProccessingService = applicantProccessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Applicant> AddApplicantAsync(Applicant applicant) =>
        TryCatch(async () =>
        {
            return await this.applicantProccessingService.AddApplicantAsync(applicant);
        });

        public IQueryable<Applicant> RetrieveAllApplicants() =>
            this.applicantProccessingService.RetrieveAllApplicants();
    }
}
