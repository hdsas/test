using BackEndTest.Enums;
using BackEndTest.Models;
using BackEndTest.Models.Responses;
using BackEndTest.RepositoryModels;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
    public class CsvService : ICsvService
    {
        private readonly ITransactionService _transactionService;

        public CsvService(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public UploadResponse ExtractData(IFormFile file)
        {
            var response = new UploadResponse();
            var records = new List<Transaction>();
            var errorMessage = string.Empty;
            bool status = true;

            List<string> csvStatus = new List<string>();

            foreach (string name in Enum.GetNames(typeof(CSVStatus)))
            {
                csvStatus.Add(name);
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false,

            };
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, config))
            {
               
                while (csv.Read())
                {

                    var valueList = new List<string>();
                    for (int i = 0; i < 5; i++)
                    {
                        if (ValidateCsvField(csv, i, valueList, out errorMessage))
                        {
                            status = false;
                            break;
                        }
                    }



                    if (!csvStatus.Contains(valueList[4]))
                    {
                        errorMessage = "Invalid Status";
                        status = false;
                    }

                    if (!status)
                        break;

                    var record = new Transaction
                    {
                        Id = valueList[0],
                        Amount = Convert.ToDecimal(valueList[1]),
                        CurrenctCode = valueList[2],
                        TransectionDate = DateTime.ParseExact(valueList[3],"dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        Status = valueList[4],
                    };


                    records.Add(record);
                }
            }

            if (records.Any())
                response.Transactions = _transactionService.Adds(records);

            response.IsValid = status;
            response.ErrorMessage = errorMessage ;
     
            return response;
        }


        private bool ValidateCsvField(CsvReader csv, int index, List<string> vakueList, out string errorMessage)
        {
            string[] messages = { "Id Field is required",
                "Amount Field is required",
                "Currency Code Field is required",
                "Transaction Date is required",
                "Status Date is required",
            };

            var value = csv.GetField(index);
            var invalid = string.IsNullOrWhiteSpace(value);
            errorMessage = invalid ? messages[index] : string.Empty;
            if (!invalid)
                vakueList.Add(value);
            return invalid;
        }
    }
}
