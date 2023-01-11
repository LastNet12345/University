using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using University.Core;
using University.Validations;

namespace University.Models
{
#nullable disable
    public class StudentCreateViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [KalleAnka(ErrorMessage = "Måste heta Kalle Anka!")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string AddressStreet { get; set; }
        public string AddressZipCode { get; set; }
        public string AddressCity { get; set; }
    }
}
