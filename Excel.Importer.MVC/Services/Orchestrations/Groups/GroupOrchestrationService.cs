﻿//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Brokers.Loggings;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Proccessings.Groups;

namespace Excel.Importer.MVC.Services.Orchestrations.Groups
{
    public partial class GroupOrchestrationService : IGroupOrchestrationService
    {
        private readonly IGroupProccessingService groupProccessingService;
        private readonly ILoggingBroker loggingBroker;

        public GroupOrchestrationService(
            IGroupProccessingService groupProccessingService,
            ILoggingBroker loggingBroker)
        {
            this.groupProccessingService = groupProccessingService;
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

        public ValueTask<Group> UpdateGroupAsync(Group group) =>
            this.groupProccessingService.UpdateGroupAsync(group);

        public ValueTask<Group> DeleteGroupAsync(Group group) =>
            this.groupProccessingService.DeleteGroupAsync(group);
    }
}
