//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Excel.Importer.MVC.Models.Foundations.Applicants;
using Excel.Importer.MVC.Models.Foundations.Applicants.Exceptions;

namespace Excel.Importer.MVC.Services.Foundations.Applicants
{
    public partial class ApplicantService
    {
        private void ValidateApplicantOnAdd(Applicant applicant)
        {
            ValidateApplicantNotNull(applicant);

            Validate(
                (Rule: IsInvalid(applicant.Id), Parameter: nameof(Applicant.Id)),
                (Rule: IsInvalid(applicant.FirstName), Parameter: nameof(Applicant.FirstName)),
                (Rule: IsInvalid(applicant.LastName), Parameter: nameof(Applicant.LastName)),
                (Rule: IsInvalid(applicant.Email), Parameter: nameof(Applicant.Email)),
                (Rule: IsInvalid(applicant.PhoneNumber), Parameter: nameof(Applicant.PhoneNumber)),
                (Rule: IsInvalid(applicant.GroupId), Parameter: nameof(Applicant.GroupId)),
                (Rule: IsInvalid(applicant.GroupName), Parameter: nameof(Applicant.GroupName)));
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {

            Condition = System.String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private void ValidateApplicantNotNull(Applicant applicant)
        {
            if (applicant == null)
                throw new NullApplicantException();
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidApplicantException = new InvalidApplicantException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidApplicantException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidApplicantException.ThrowIfContainsErrors();
        }
    }
}
