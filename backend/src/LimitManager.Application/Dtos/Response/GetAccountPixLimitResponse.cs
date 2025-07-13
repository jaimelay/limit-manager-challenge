namespace LimitManager.Application.Dtos.Response;

public record GetAccountPixLimitResponse(string Cpf, string Agency, string AccountNumber, decimal PixLimit);