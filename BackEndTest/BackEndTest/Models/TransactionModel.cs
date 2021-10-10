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


        public decimal Amount { get; set; }


        public string CurrenctCode { get; set; }


        public DateTime TransectionDate { get; set; }

        public string Status { get; set; }
    }
}
