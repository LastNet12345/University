﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using University.Core;

namespace University.Models
{
#nullable disable
    public class StudentCreateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressStreet { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressCity { get; set; }
    }
}
