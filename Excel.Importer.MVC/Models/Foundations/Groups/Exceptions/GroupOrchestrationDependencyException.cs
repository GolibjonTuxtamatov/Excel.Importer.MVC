//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Orchestrations.Groups.Exceptions
{
    public class GroupOrchestrationDependencyException : Xeption
    {
        public GroupOrchestrationDependencyException(Xeption innerException)
            : base("Group orchestration dependency error occured, contact support")
        { }
    }
}
