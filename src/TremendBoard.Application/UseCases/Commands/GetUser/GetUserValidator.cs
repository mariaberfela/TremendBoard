using FluentValidation;

namespace TremendBoard.Application.UseCases.Commands.GetFullName
{
    public class GetUserValidator: AbstractValidator<GetUserRequest>
    {
        public GetUserValidator()
        {
            RuleFor(r => r.Id).NotEmpty();
        }
    }
}
