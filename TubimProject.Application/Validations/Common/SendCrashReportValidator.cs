using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.DTOs.Common;
using TubimProject.Application.Features.CrashReport.Command.Create;

namespace TubimProject.Application.Validations.Common
{
    public class SendCrashReportValidator : AbstractValidator<CreateCrashReportCommand>
    {
        public SendCrashReportValidator()
        {
            RuleFor(r => r.Username).NotNull().WithMessage("Kullanıcı Adı Boş Olaamz");
            RuleFor(r => r.ErrorDescription).NotNull().WithMessage("Hata Açıklaması Boş Olamaz").MinimumLength(10).WithMessage("Hata Açıklaması en az 10 Karakter Olmalıdır.");
        }
    }
}
