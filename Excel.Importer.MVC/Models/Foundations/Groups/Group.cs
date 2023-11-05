//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.ComponentModel.DataAnnotations;

namespace Excel.Importer.MVC.Models.Foundations.Groups
{
    public class Group
    {
        public Guid Id { get; set; }
        [Required]
        public string GroupName { get; set; }
    }
}
