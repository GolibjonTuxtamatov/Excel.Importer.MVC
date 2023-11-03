//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Linq;
using System.Threading.Tasks;
using Excel.Importer.MVC.Models.Foundations.Applicants;

namespace Excel.Importer.MVC.Services.Proccessings.Applicants
{
    public interface IApplicantProccessingService
    {
        ValueTask<Applicant> AddApplicantAsync(Applicant applicant);

        IQueryable<Applicant> RetrieveAllApplicants();
    }
}
