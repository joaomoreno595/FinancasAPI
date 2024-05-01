using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Transaction
{
    public int TransactionId { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }

    [AllowedValues("C", "D")]
    public string CreditOrDebit { get; set; } = "C";

    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}
