using DocsHub.WebAPI.Dtos.User;
using FluentValidation;

namespace DocsHub.WebAPI.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto> {
    public CreateUserDtoValidator() {
        RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório")
        .Length(3, 200).WithMessage("O nome deve ter entre 3 e 200 caracteres");

        RuleFor(x => x.Email).NotEmpty().WithMessage("O e-mail é obrigatório")
        .EmailAddress().WithMessage("O e-mail informado não é válido");

        RuleFor(x => x.Password).NotEmpty().WithMessage("A senha é obrigatória")
        .Length(6, 100).WithMessage("A senha deve ter entre 6 e 20 caracteres");

        RuleFor(x => x.Role).IsInEnum().WithMessage("O perfil é obrigatório");
    }
}