//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Proccessings.Groups.Exceptions
{
    public class GroupProccessingDependencyValidationException : Xeption
    {
        public GroupProccessingDependencyValidationException(Xeption innerException)
            : base("Group proccessing dependency validation error occured, contact support",
                 innerException)
        { }
    }
}
