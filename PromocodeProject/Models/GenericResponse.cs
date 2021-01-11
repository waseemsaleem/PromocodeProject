using System.Collections.Generic;
using System.Net;

namespace PromoCodeProject.Models
{
    public class GenericResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public IList<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}