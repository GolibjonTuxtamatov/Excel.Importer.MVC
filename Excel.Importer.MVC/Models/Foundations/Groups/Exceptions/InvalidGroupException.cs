//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Foundations.Groups.Exceptions
{
    public class InvalidGroupException : Xeption
    {
        public InvalidGroupException()
            : base("Group is invalid")
        { }
    }
}
