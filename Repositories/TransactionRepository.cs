using Domain.Abstractions;
using Domain.Entities;
using Financeiro.DTOs;
using Identity.Context;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Repositories;

public class TransactionRepository(AppDbContext context) : ITransactionRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Transaction> AddTransaction(Transaction transaction)
    {
        _context.Set<Transaction>().Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<bool> DeleteTransaction(int transactionId)
    {
        var transaction = await GetTransactionById(transactionId);
        if (transaction is null)
            return false;

        _context.Set<Transaction>().Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Transaction>?> GetAll() => await _context.Set<Transaction>().ToListAsync();

    public async Task<IEnumerable<Transaction>?> GetAllByCategory(int categoryId) => await _context.Set<Transaction>().Where(t => t.CategoryId == categoryId).ToListAsync();

    public async Task<Transaction?> GetTransactionById(int id) => await _context.Set<Transaction>().FirstOrDefaultAsync(p => p.TransactionId == id);

    public async Task<TransactionTotal> TotalByCategoryId(int categoryId)
    {
        TransactionTotal transactionTotal = new()
        {
            CreditTotal = await _context.Set<Transaction>().Where(t => t.CategoryId == categoryId && t.CreditOrDebit == "C").SumAsync(t => t.Valor),
            DebitTotal = await _context.Set<Transaction>().Where(t => t.CategoryId == categoryId && t.CreditOrDebit == "D").SumAsync(t => t.Valor)
        };

        return transactionTotal;
    }

    public async Task<Transaction> UpdateTransaction(Transaction transaction)
    {
        _context.Set<Transaction>().Update(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}
