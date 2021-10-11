using BackEndTest.Enums;
using BackEndTest.Extention;
using BackEndTest.Models;
using BackEndTest.Models.Requests;
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
            foreach (var trans in transactions)
            {

                if (trans.Status == XmlStatus.Approved.Description() || trans.Status == CSVStatus.Approved.Description())
                {
                    trans.Status = "A";
                }
                else if (trans.Status == XmlStatus.Rejected.Description() || trans.Status == CSVStatus.Failed.Description())
                {
                    trans.Status = "R";
                }
                else if (trans.Status == XmlStatus.Done.Description() || trans.Status == CSVStatus.Finished.Description())
                {
                    trans.Status = "D";
                }


                _context.Transaction.Add(trans);

            }

            _context.SaveChanges();

            return transactions;
        }

        public IEnumerable<TransactionModel> Search(SearchRequest search)
        {
            var query = _context.Transaction as IQueryable<Transaction>;

            if (!string.IsNullOrWhiteSpace(search.Id))
            {
                query = query.Where(t => t.Id == search.Id);
            }

            if (search.Date.HasValue)
            {
                query = query.Where(t => t.TransectionDate.Date == search.Date.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(search.StatusCode))
            {
               query = query.Where(t => t.Status == search.StatusCode);
            }

            var result = query.ToList().Select(r => new TransactionModel
            {
                Id = r.Id,
                Payment = $"{r.Amount} {r.CurrenctCode}",
                Status = r.Status.ToUpper()

            });

            return result;

        }
    }
}
