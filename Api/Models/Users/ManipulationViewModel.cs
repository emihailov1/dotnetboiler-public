using Api.Resources;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Users
{
    public abstract class ManipulationViewModel
    {
        [Required(ErrorMessage = ResourceKeys.RequiredFirstname)]
        [MaxLength(200, ErrorMessage = ResourceKeys.LongFirstname)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = ResourceKeys.RequiredLastname)]
        [MaxLength(200, ErrorMessage = ResourceKeys.LongLastname)]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = ResourceKeys.InvalidEmail)]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = ResourceKeys.RequiredRole)]
        public string Role { get; set; }

        [Required(ErrorMessage = ResourceKeys.RequiredStatus)]
        public string Status { get; set; }
    }
}
