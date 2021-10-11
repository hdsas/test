using BackEndTest.Models.Responses;
using BackEndTest.RepositoryModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace BackEndTest.Services
{
    public class XmlService : IXmlService
    {
        private readonly ITransactionService _transactionService;

        public XmlService(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public UploadResponse ExtractData(IFormFile file )
        {
            var response = new UploadResponse();
            response.IsValid = true;
            try
            {
                

                var path = new Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                var filePath = Path.Combine(path, file.FileName).ToString();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add("", path + "\\Schema\\Schema.xsd");
                settings.ValidationType = ValidationType.Schema;
                XmlDocument document = new XmlDocument();
                XmlReader reader = XmlReader.Create(file.OpenReadStream(), settings);
                document.Load(reader);

                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);
                document.Validate(eventHandler);

                var transections = new List<Transaction>();
                XmlNodeList transactionList = document.GetElementsByTagName("Transaction");
                foreach (XmlNode tran in transactionList)
                {

                    var id = tran.Attributes["id"]?.Value;
                    var amount = tran.SelectSingleNode("PaymentDetails/Amount").InnerText;
                    var currencyCode = tran.SelectSingleNode("PaymentDetails/CurrencyCode").InnerText;
                    var transactionDate = tran.SelectSingleNode("TransactionDate").InnerText;
                    var status = tran.SelectSingleNode("Status").InnerText;

                    transections.Add(new Transaction
                    {
                        Id = id,
                        Amount = Convert.ToDecimal(amount),
                        CurrenctCode = currencyCode,
                        TransectionDate = DateTime.Parse(transactionDate),
                        Status = status
                    });

                }


                if(transections.Any())
                    response.Transactions = _transactionService.Adds(transections);




            }
            catch (Exception ex)
            {
                response.IsValid = false;
                response.ErrorMessage = ex.Message;

            }
            return response;
        }

        private void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }
    }


}
