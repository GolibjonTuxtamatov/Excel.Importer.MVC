//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;

namespace Excel.Importer.MVC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        string DownloadExcel(Guid id);
    }
}
