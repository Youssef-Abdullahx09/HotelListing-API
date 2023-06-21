using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class RequestParams
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; }
        private int _pageSize = 10;

        public int PageSize 
        { 
            get
            {
                return _pageSize;
            }
            set 
            {
                _pageSize = (value > 10) ? maxPageSize : value;
            }
        }
    }
}
