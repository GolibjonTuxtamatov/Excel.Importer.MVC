//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Excel.Importer.MVC.Services.Foundations.Groups.Exceptions
{
    public class FaildStorageGroupException : Xeption
    {
        public FaildStorageGroupException(Exception innerException)
            : base("Failed group storage error occured, contact support",
                 innerException)
        { }
    }
}
