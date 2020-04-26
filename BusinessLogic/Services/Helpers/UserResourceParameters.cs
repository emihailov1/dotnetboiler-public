using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Helpers
{
    public class UserResourceParameters : ResourceParameters
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public override string OrderBy { get; set; } = "Id";

    }
}
