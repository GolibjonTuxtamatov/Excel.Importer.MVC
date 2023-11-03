//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Microsoft.EntityFrameworkCore;

namespace Excel.Importer.MVC.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Group> Groups { get; set; }

        public ValueTask<Group> InsertGroupAsync(Group group) =>
            InsertAsync(group);

        public IQueryable<Group> SelectAllGroup() =>
            SelectAll<Group>();

        public ValueTask<Group> SelectGroupById(Guid id) =>
            SelectAsync<Group>(id);

        public ValueTask<Group> UpdateGroupAsync(Group group) =>
            UpdateAsync(group);

        public ValueTask<Group> DeleteGroupAsync(Group group) =>
            DeleteAsync(group);
    }
}
