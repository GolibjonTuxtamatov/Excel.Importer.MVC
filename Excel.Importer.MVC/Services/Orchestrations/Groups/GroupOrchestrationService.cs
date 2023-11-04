//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Brokers.Loggings;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Orchestrations.Applicants;
using Excel.Importer.MVC.Services.Proccessings.Applicants;
using Excel.Importer.MVC.Services.Proccessings.Groups;

namespace Excel.Importer.MVC.Services.Orchestrations.Groups
{
    public partial class GroupOrchestrationService : IGroupOrchestrationService
    {
        private readonly IGroupProccessingService groupProccessingService;
        IApplicantProccessingService applicantProccessingService;
        private readonly ILoggingBroker loggingBroker;

        public GroupOrchestrationService(
            IGroupProccessingService groupProccessingService,
            IApplicantProccessingService applicantProccessingService,
            ILoggingBroker loggingBroker)
        {
            this.groupProccessingService = groupProccessingService;
            this.applicantProccessingService = applicantProccessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Group> AddGroupAsync(Group group) =>
        TryCatch(async () =>
        {
            return await this.groupProccessingService.AddGroupAsync(group);
        });

        public IQueryable<Group> RetrieveAllGroups() =>
            TryCatch(() => this.groupProccessingService.RetrieveAllGroups());

        public ValueTask<Group> RetrieveGroupByIdAsync(Guid id) =>
            this.groupProccessingService.RetrieveGroupByIdAsync(id);

        public ValueTask<Group> UpdateGroupAsync(Group group)
        {
            StudentUpdate(group);

            return this.groupProccessingService.UpdateGroupAsync(group);
        }

        public ValueTask<Group> DeleteGroupAsync(Group group) =>
            this.groupProccessingService.DeleteGroupAsync(group);

        private void StudentUpdate(Group group)
        {
            IQueryable<Applicant> applicants =
                this.applicantProccessingService.RetrieveAllApplicants();

            foreach (Applicant applicant in applicants)
            {
                if(applicant.GroupId == group.Id)
                {
                    applicant.GroupId = group.Id;
                    applicant.GroupName = group.GroupName;
                    this.applicantProccessingService
                }
            }

        }
    }
}
