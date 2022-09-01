using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.DTOs.Identity.User;

namespace TubimProject.Application.Validations.Management
{
    public class CreateNewUserValidator : AbstractValidator<AddNewUser>
    {
        public CreateNewUserValidator()
        {
            RuleFor(x => x.CepTel).NotEmpty().WithMessage("Mobile Phone Number cannot be empty!");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be empty!");
        }
    }
}
