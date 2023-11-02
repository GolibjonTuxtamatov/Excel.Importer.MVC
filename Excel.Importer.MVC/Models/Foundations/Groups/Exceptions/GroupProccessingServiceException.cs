//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Proccessings.Groups.Exceptions
{
    public class GroupProccessingServiceException : Xeption
    {
        public GroupProccessingServiceException(Xeption innerException)
            : base("Group proccessing service error occured, contact support",
                 innerException)
        { }
    }
}
