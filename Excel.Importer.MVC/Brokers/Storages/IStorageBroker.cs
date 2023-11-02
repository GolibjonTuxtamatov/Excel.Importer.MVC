//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;

namespace Excel.Importer.MVC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<T> InsertAsync<T>(T @object);

        IQueryable<T> SelectAll<T>() where T : class;
    }
}
