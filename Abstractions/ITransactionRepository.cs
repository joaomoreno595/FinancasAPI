using Domain.Entities;
using Financeiro.DTOs;

namespace Domain.Abstractions
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>?> GetAll();
        Task<IEnumerable<Transaction>?> GetAllByCategory(int categoryId);
        Task<Transaction?> GetTransactionById(int transactionId);
        Task<Transaction> AddTransaction(Transaction transaction);
        Task<Transaction> UpdateTransaction(Transaction transaction);
        Task<bool> DeleteTransaction(int transactionId);
        Task<TransactionTotal> TotalByCategoryId(int categoryId);
    }
}
