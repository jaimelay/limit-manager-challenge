namespace LimitManager.Application.Dtos.Requests;

public record DeleteAccountRequest(string Cpf, string Agency, string AccountNumber);