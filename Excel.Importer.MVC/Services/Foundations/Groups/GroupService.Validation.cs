//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Excel.Importer.MVC.Models.Foundations.Groups;
using Excel.Importer.MVC.Services.Foundations.Groups.Exceptions;

namespace Excel.Importer.MVC.Services.Foundations.Groups
{
    public partial class GroupService
    {
        private void ValidateGroupOnAdd(Group group)
        {
            ValidateGroupNotNull(group);

            Validate(
                (Rule: IsInvalid(group.Id), Parameter: nameof(Group.Id)),
                (Rule: IsInvalid(group.GroupName), Parameter: nameof(Group.GroupName)));
        }

        private void ValidateGroupNotNull(Group group)
        {
            if (group == null)
                throw new NullGroupException();
        }

        private dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidGroupException = new InvalidGroupException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidGroupException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidGroupException.ThrowIfContainsErrors();
        }
    }
}
