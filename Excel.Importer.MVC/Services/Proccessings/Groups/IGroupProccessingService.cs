//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Groups;

namespace Excel.Importer.MVC.Services.Proccessings.Groups
{
    public interface IGroupProccessingService
    {
        ValueTask<Group> AddGroupAsync(Group group);
        IQueryable<Group> RetrieveAllGroups();
        ValueTask<Guid> EnsureExistGroupAsync(string groupName);
    }
}
