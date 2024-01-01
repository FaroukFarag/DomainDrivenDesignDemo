using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() 
        {
            RuleFor(rc => rc.FirstName).NotEmpty();
            RuleFor(rc => rc.LastName).NotEmpty();
            RuleFor(rc => rc.Email).NotEmpty();
            RuleFor(rc => rc.Password).NotEmpty();
        }
    }
}
