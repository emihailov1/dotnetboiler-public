using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services.Helpers
{
    public class ResourceParameters
    {
        const int maxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;

        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string SearchQuery { get; set; }

        public int Id { get; set; }

        public virtual string OrderBy { get; set; }

    }
}
