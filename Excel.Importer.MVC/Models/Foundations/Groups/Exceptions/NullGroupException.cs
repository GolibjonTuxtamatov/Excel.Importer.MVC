//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Excel.Importer.MVC.Services.Foundations.Groups.Exceptions
{
    public class NullGroupException : Xeption
    {
        public NullGroupException()
            : base("Group is null")
        { }
    }
}
