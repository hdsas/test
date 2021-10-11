
using BackEndTest.Models;
using BackEndTest.Models.Requests;
using BackEndTest.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
    public interface ITransactionService
    {
        Transaction Add(Transaction transaction);

        IEnumerable<Transaction> Adds(IEnumerable<Transaction> transaction);

        IEnumerable<TransactionModel> Search(SearchRequest search);
    }
}
