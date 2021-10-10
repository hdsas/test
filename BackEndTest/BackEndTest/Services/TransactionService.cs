using BackEndTest.RepositoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }
        public Transaction Add(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            _context.SaveChanges();

            return transaction;
        }

        public IEnumerable<Transaction> Adds(IEnumerable<Transaction> transactions)
        {
            foreach (var trans in transactions) {
                _context.Transaction.Add(trans);

            }

            _context.SaveChanges();

            return transactions;
        }
    }
}
