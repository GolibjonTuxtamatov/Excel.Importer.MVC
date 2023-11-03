//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Groups;

namespace Excel.Importer.MVC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Group> InsertGroupAsync(Group group);

        IQueryable<Group> SelectAllGroup();

        ValueTask<Group> SelectGroupById(Guid id);

        ValueTask<Group> UpdateGroupAsync(Group group);

        ValueTask<Group> DeleteGroupAsync(Group group);
    }
}
