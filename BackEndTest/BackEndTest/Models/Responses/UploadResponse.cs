using BackEndTest.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackEndTest.Models.Responses
{
    public class UploadResponse
    {
        public bool IsValid { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

    }
}
