using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Models
{
    public class TransactionModel
    {

      
        public string Id { get; set; }

        public string Payment { get; set; }

        public string Status { get; set; }
    }
}
