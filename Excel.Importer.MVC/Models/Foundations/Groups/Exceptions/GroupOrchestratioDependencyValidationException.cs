//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Orchestrations.Groups.Exceptions
{
    public class GroupOrchestratioDependencyValidationException : Xeption
    {
        public GroupOrchestratioDependencyValidationException(Xeption innerException)
            : base("Group orchestration dependency validation error occured, fix the error and try again",
                 innerException)
        { }
    }
}
