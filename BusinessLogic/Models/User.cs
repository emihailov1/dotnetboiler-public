using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime Created { get; set; }

        public string Role { get; set; }

        public Status Status { get; set; }

    }
}
