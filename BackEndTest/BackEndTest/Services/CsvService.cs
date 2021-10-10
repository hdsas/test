﻿using BackEndTest.Models;
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
        public bool ExtractData(IFormFile file, out string errorMessage)
        {
            var records = new List<Transaction>();
            errorMessage = string.Empty;
            bool status = true;
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
                _transactionService.Adds(records);

            return status;
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
