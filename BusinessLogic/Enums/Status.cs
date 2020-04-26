
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Enums
{
    public enum Status
    {
        [Display(Name = "Active")]
        Active,
        [Display(Name = "Inactive")]
        Inactive,
        [Display(Name = "Banned")]
        Banned
    }
}
