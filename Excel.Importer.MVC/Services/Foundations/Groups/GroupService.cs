//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Brokers.Loggings;
using Excel.Importer.MVC.Brokers.Storages;
using Excel.Importer.MVC.Models.Foundations.Groups;

namespace Excel.Importer.MVC.Services.Foundations.Groups
{
    public partial class GroupService : IGroupService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public GroupService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Group> AddGroupAsync(Group group) =>
            TryCatch(async () =>
            {
                ValidateGroupOnAdd(group);

                return await this.storageBroker.InsertGroupAsync(group);
            });

        public IQueryable<Group> RetrieveAllGroups() =>
            TryCatch(() => this.storageBroker.SelectAllGroup());
    }
}
