using System.ComponentModel.DataAnnotations;

namespace LimitManager.Application.Dtos.Requests;

public record CreateAccountRequest([Required] string Cpf, [Required] string Agency, [Required] string AccountNumber, [Required] decimal Balance, [Required] decimal PixLimit);