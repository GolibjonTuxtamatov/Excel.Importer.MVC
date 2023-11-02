//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Orchestrations.Groups.Exceptions
{
    public class GroupOrchestrationValidationException : Xeption
    {
        public GroupOrchestrationValidationException(Xeption innerException)
            : base("Group orchestration validation error occured, fix the error and try again",
                 innerException)
        { }
    }
}
