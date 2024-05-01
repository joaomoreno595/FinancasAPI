using Domain.Abstractions;
using Domain.Entities;
using Financeiro.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController(ITransactionRepository transactionRepository) : ControllerBase
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> Get()
    {
        IEnumerable<Transaction>? transactions = await _transactionRepository.GetAll();
        if (transactions == null)
            return NotFound();

        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetById(int id)
    {
        Transaction? transaction = await _transactionRepository.GetTransactionById(id);
        if (transaction == null)
            return NotFound("Transaction not found");

        return Ok(transaction);
    }

    [HttpGet("{categoryId:int}")]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetAllByCategory(int categoryId)
    {
        IEnumerable<Transaction>? transactions = await _transactionRepository.GetAllByCategory(categoryId);
        if (transactions == null)
            return NotFound();

        return Ok(transactions);
    }

    [HttpGet("total/{categoryId:int}")]
    public async Task<ActionResult<TransactionTotal>> TotalByCategoryId(int categoryId) => Ok(await _transactionRepository.TotalByCategoryId(categoryId));

    [HttpPut("{id}")]
    public async Task<ActionResult<Transaction>> Update(int id, Transaction transaction)
    {
        if (transaction == null)
            return NotFound();

        if (id != transaction.TransactionId)
            return BadRequest("id diferente do objeto");

        await _transactionRepository.UpdateTransaction(transaction);
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> Post(Transaction transaction)
    {
        if (transaction == null)
            return NotFound("Transaction not found");

        await _transactionRepository.AddTransaction(transaction);
        return Ok(transaction);
    }

    [HttpDelete("{id:int}")]
    [Authorize("Admin")]
    public async Task<ActionResult<Transaction>> Delete(int id)
    {
        if (await _transactionRepository.DeleteTransaction(id))
            return Ok();

        return NotFound();
    }
}
