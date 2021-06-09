using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WavoProjects.Api.Models
{
    public class GenericResponse<T> where T : class
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; } = true;

        public GenericResponse()
        {

        }

        public GenericResponse(T data)
        {
            Data = data;
            Success = true;
        }

        public GenericResponse(string error)
        {
            Success = false;
            Error = error;
        }
    }
}
