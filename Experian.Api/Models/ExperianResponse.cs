using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Experian.Api
{
    public class ExperianResponse
    {
        public string Path { get; set; }
        public DateTime TimeStamp { get; set; }
        public HttpStatusCode Status { get; set; }
        public List<Applicant> Data { get; set; } = new List<Applicant>();
        public string ErrorMessage { get; set; }
    }
}
