//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Foundations.Groups.Exceptions
{
    public class GroupDependencyException : Xeption
    {
        public GroupDependencyException(Xeption innerException)
            : base("Group dependency error occured, fix the error try again",
                 innerException)
        { }

    }
}
