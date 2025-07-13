namespace LimitManager.Application.Dtos.Requests;

public record UpdateAccountPixLimitRequest(string Cpf, string Agency, string AccountNumber, decimal PixLimit);