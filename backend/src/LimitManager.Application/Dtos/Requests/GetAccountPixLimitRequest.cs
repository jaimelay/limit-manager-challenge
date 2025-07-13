namespace LimitManager.Application.Dtos.Requests;

public record GetAccountPixLimitRequest(string Cpf, string Agency, string AccountNumber);