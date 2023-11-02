//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Foundations.Groups.Exceptions
{
    public class GroupValidationException : Xeption
    {
        public GroupValidationException(Xeption innerException)
            : base("Group validation error occured, fix the error try again",
                 innerException)
        { }
    }
}
