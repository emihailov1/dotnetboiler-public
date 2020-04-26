using Api.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Users
{
    public class CreateViewModel : ManipulationViewModel
    {
        [MinLength(8, ErrorMessage = ResourceKeys.Required8Symbols)]
        public string Password { get; set; }
        [MinLength(8, ErrorMessage = ResourceKeys.Required8Symbols)]
        [Compare("Password", ErrorMessage = ResourceKeys.PasswordsDoNotMatch)]
        public string PasswordRepeat { get; set; }
        [Required(ErrorMessage = ResourceKeys.RequiredEmail)]
        public override string Email { get; set; }
    }
}
