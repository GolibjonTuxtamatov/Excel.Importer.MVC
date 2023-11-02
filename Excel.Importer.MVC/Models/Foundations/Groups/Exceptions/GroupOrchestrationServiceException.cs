//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Orchestrations.Groups.Exceptions
{
    public class GroupOrchestrationServiceException : Xeption
    {
        public GroupOrchestrationServiceException(Xeption innerException)
            : base("Group orchestration service error occured, contact support",
                 innerException)
        { }
    }
}
