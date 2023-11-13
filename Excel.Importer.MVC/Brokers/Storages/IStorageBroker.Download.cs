//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Microsoft.AspNetCore.Mvc;

namespace Excel.Importer.MVC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        string DownloadExcel(Guid id);
    }
}
