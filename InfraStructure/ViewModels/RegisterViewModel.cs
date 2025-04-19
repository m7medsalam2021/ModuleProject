using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.ViewModels
{
    public class RegisterViewModel 
    {
        [Required(ErrorMessage = "FullName Should Be here")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Email should be Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password Not Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
