using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Models.Requests
{
    public class SearchRequest
    {
        public string Id { get; set; }

        public string StatusCode { get; set; }

        public DateTime? Date { get; set; }
    }
}
