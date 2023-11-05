//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.ComponentModel.DataAnnotations;

namespace Excel.Importer.MVC.Models.Foundations.Applicants
{
    public class Applicant
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$",ErrorMessage ="Email is invalid")]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public Guid GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }
    }
}
