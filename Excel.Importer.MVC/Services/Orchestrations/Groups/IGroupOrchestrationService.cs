//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Groups;

namespace Excel.Importer.MVC.Services.Orchestrations.Groups
{
    public interface IGroupOrchestrationService
    {
        ValueTask<Group> AddGroupAsync(Group group);
        IQueryable<Group> RetrieveAllGroups();
    }
}
