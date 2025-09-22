using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.ApiResponse
{
    public class ApiResponse<T>
    {
        public T? Results { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Status { get; set; }
    }
}
