//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Proccessings.Groups.Exceptions
{
    public class GroupProccessingDepedencyException : Xeption
    {
        public GroupProccessingDepedencyException(Xeption innerException)
            : base("Group proccessing dependency error occured, fix the error try again",
                 innerException)
        { }
    }
}
