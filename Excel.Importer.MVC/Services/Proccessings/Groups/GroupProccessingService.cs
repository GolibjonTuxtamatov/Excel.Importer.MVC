//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Brokers.Loggings;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Foundations.Groups;

namespace Excel.Importer.MVC.Services.Proccessings.Groups
{
    public partial class GroupProccessingService : IGroupProccessingService
    {
        private readonly IGroupService groupService;
        private readonly ILoggingBroker loggingBroker;

        public GroupProccessingService(IGroupService groupService, ILoggingBroker loggingBroker)
        {
            this.groupService = groupService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Group> AddGroupAsync(Group group) =>
        TryCatch(async () =>
        {
            group.Id = Guid.NewGuid();
            return await this.groupService.AddGroupAsync(group);
        });

        public IQueryable<Group> RetrieveAllGroups() =>
            TryCatch(() => this.groupService.RetrieveAllGroups());

        public ValueTask<Group> RetrieveGroupByIdAsync(Guid id) =>
            this.groupService.RetrieveGroupByIdAsync(id);

        public ValueTask<Group> UpdateGroupAsync(Group group) =>
            this.groupService.UpdateGroupAsync(group);

        public ValueTask<Group> DeleteGroupAsync(Group group) =>
            this.groupService.DeleteGroupAsync(group);

        public async ValueTask<Guid> EnsureExistGroupAsync(string groupName)
        {
            IQueryable<Group> groups = RetrieveAllGroups();

            if (EnsureExistGroupName(groupName))
            {
                Group foundGroup = groups.FirstOrDefault(group =>
                    group.GroupName == groupName);

                return foundGroup.Id;
            }
            else
            {
                var group = new Group
                {
                    Id = Guid.NewGuid(),
                    GroupName = groupName
                };
                await groupService.AddGroupAsync(group);

                return group.Id;
            }

        }

        private bool EnsureExistGroupName(string groupName)
        {
            IQueryable<Group> groups = RetrieveAllGroups();

            return groups.Contains(groups.FirstOrDefault(group =>
                group.GroupName == groupName));
        }
    }
}
