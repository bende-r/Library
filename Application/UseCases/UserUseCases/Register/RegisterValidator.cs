using FluentValidation;

namespace Application.UseCases.AuthUseCases.Register;

public class RegisterValidator: AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
    
    }
}