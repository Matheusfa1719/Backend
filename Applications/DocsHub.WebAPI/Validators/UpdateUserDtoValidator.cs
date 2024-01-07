using DocsHub.WebAPI.Dtos.User;
using FluentValidation;

namespace DocsHub.WebAPI.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto> {
    public UpdateUserDtoValidator() {
        RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório")
        .Length(3, 200).WithMessage("O nome deve ter entre 3 e 200 caracteres");

        RuleFor(x => x.Email).NotEmpty().WithMessage("O e-mail é obrigatório")
        .EmailAddress().WithMessage("O e-mail informado não é válido");

        RuleFor(x => x.Role).IsInEnum().WithMessage("O perfil é obrigatório");
    }
}