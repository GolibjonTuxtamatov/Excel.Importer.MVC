//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;

namespace Excel.Importer.MVC.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Applicant> InsertApplicantAsync(Applicant applicant);
    }
}
