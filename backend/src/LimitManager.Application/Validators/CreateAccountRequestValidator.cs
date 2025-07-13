using FluentValidation;
using LimitManager.Application.Dtos.Requests;

namespace LimitManager.Application.Validators;

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator()
    {
        RuleFor(request => request.Cpf)
            .NotEmpty()
            .WithMessage("CPF is required")
            .Length(11)
            .WithMessage("CPF must have 11 digits");
        
        RuleFor(request => request.Agency).NotEmpty().WithMessage("Agency is required");
        
        RuleFor(request => request.AccountNumber).NotEmpty().WithMessage("Account Number is required");
    }
}