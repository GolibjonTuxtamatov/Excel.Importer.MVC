//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Proccessings.Groups.Exceptions
{
    public class GroupProccessingValidationException : Xeption
    {
        public GroupProccessingValidationException(Xeption innerException)
            : base("Group proccessing validation error occured, fix the error and try again",
                 innerException)
        { }
    }
}
