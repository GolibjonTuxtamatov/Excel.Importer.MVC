//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Excel.Importer.MVC.Services.Foundations.Groups.Exceptions
{
    public class AlreadyExistGroupException : Xeption
    {
        public AlreadyExistGroupException(Exception innerException)
            : base("Group already exist",
                 innerException)
        { }
    }
}
