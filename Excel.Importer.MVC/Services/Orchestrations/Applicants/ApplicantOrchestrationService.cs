//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Brokers.Loggings;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Orchestrations.Groups;
using Excel.Importer.MVC.Services.Proccessings.Applicants;

namespace Excel.Importer.MVC.Services.Orchestrations.Applicants
{
    public partial class ApplicantOrchestrationService : IApplicantOrchestrationService
    {
        private readonly IApplicantProccessingService applicantProccessingService;
        private readonly IGroupOrchestrationService groupOrchestrationService;
        private readonly ILoggingBroker loggingBroker;

        public ApplicantOrchestrationService(
            IApplicantProccessingService applicantProccessingService,
            IGroupOrchestrationService groupOrchestrationService,
            ILoggingBroker loggingBroker)
        {
            this.applicantProccessingService = applicantProccessingService;
            this.groupOrchestrationService = groupOrchestrationService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Applicant> AddApplicantAsync(Applicant applicant) =>
        TryCatch(async () =>
        {
            Applicant existGroupApplicant = AddApplicantIfGroupExist(applicant);
            return await this.applicantProccessingService.AddApplicantAsync(applicant);
        });

        public async ValueTask<Applicant> AddApplicantInGroup(Applicant applicant)
        {
            applicant.Id = Guid.NewGuid();
            return await this.applicantProccessingService.AddApplicantAsync(applicant);
        }

        public IQueryable<Applicant> RetrieveAllApplicants() =>
            this.applicantProccessingService.RetrieveAllApplicants();

        public ValueTask<Applicant> RetrieveApplicantByIdAsync(Guid Id) =>
            this.applicantProccessingService.RetriveApplicantByIdAsync(Id);

        public ValueTask<Applicant> ModifyApplicantAsync(Applicant applicant) =>
            this.applicantProccessingService.ModifyApplicantAsync(applicant);

        public ValueTask<Applicant> RemoveApplicantAsync(Applicant applicant) =>
            this.applicantProccessingService.RemoveApplicantAsync(applicant);

        private Applicant AddApplicantIfGroupExist(Applicant applicant)
        {
            ValidateApplicantForGroup(applicant);

            IQueryable<Group> groups =
                this.groupOrchestrationService.RetrieveAllGroups();

            foreach (Group group in groups)
            {
                if (group.GroupName == applicant.GroupName)
                {
                    applicant.GroupId = group.Id;
                    applicant.GroupName = group.GroupName;
                    break;
                }
            }
            applicant.Id = Guid.NewGuid();

            return applicant;
        }

        private void ValidateApplicantForGroup(Applicant applicant)
        {
            IQueryable<Group> groups =
               this.groupOrchestrationService.RetrieveAllGroups();

            if (!groups.Contains(groups.FirstOrDefault(group => group.GroupName == applicant.GroupName)))
            {
                throw new ApplicantExistGroupException();
            }
        }
    }
}
