using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.ApiResponse;

namespace Utility.Response
{
    // Make the class generic by adding <T>
    public class PagedWithResponse<T>
    {
        public T? Result { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public int TotalCount { get; set; } = 0;
    }
}
