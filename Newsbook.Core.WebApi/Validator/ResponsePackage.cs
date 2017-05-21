using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newsbook.Core.WebApi.Validator
{
    public class ResponsePackage
    {
        public List<string> Errors { get; set; }

        public object Result { get; set; }

        public System.Net.HttpStatusCode StatusCode { get; set; }
        public string StatusCodeDescription { get { return StatusCode.ToString(); } }

        public ResponsePackage(object result, List<string> errors, System.Net.HttpStatusCode status)
        {
            Errors = errors;
            Result = result;
            StatusCode = status;
        }
    }
}