namespace LimitManager.Application.Dtos.Requests;

public record CreatePixTransactionRequest
{
    public required Account FromAccount { get; init; }
    public required Account ToAccount { get; init; }
    public required decimal Amount { get; init; }

    public class Account
    {
        public required string Cpf { get; init; }
        public required string Agency { get; init; }
        public required string AccountNumber { get; init; }
    }
};