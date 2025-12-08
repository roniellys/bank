using System.Security.Cryptography.X509Certificates;
using Bank.Domain.Entities;
using FluentValidation;

namespace Bank.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x)
            .NotEmpty()
            .WithMessage("The entity cannot be empty.")

            .NotNull()
            .WithMessage("The entity cannot be null.");

            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The name cannot be empty.")

            .NotNull()
            .WithMessage("The name cannot be null.")

            .MinimumLength(3)
            .WithMessage("O nome deve ter no mínimo 3 caracteres")

            .MaximumLength(80)
            .WithMessage("O nome deve ter no máximo 80 caracteres");

            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("The email cannot be empty.")

            .NotNull()
            .WithMessage("The email cannot be null.")

            .MinimumLength(10)
            .WithMessage("O email deve ter no mínimo 10 caracteres")

            .MaximumLength(180)
            .WithMessage("O email deve ter no máximo 180 caracteres")

            .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
            .WithMessage("O email informado não é valido.");

            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("The password cannot be empty.")

            .NotNull()
            .WithMessage("The password cannot be null.")

            .MinimumLength(6)
            .WithMessage("O password deve ter no mínimo 6 caracteres")

            .MaximumLength(30)
            .WithMessage("O password deve ter no máximo 30 caracteres");
        }
    }
}