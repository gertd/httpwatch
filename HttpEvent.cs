using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWatch
{
    class HttpEvent
    {
        public Uri Url { get; set; }
        public HttpVerb Verb { get; set; }
        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int Status { get; set; }
    }
}
