using FluentValidation;
using Poliedro.Psr.Application.Markers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poliedro.Psr.Application.Markers.Validators
{
    public class AddMarkerToReaderCommandValidator : AbstractValidator<AddMarkerToReaderCommand>
    {
        public AddMarkerToReaderCommandValidator()
        {
            RuleFor(x => x.ReaderGuid)
                .NotNull().WithMessage("The name cannot be null.")
                .NotEmpty().WithMessage("The name cannot be empty.");

            RuleFor(x => x.Marker.Code)
               .NotNull().WithMessage("The name cannot be null.")
               .NotEmpty().WithMessage("The name cannot be empty.");

            RuleFor(x => x.Marker.Trt)
               .NotNull().WithMessage("The name cannot be null.")
               .NotEmpty().WithMessage("The name cannot be empty.");

            RuleFor(x => x.Marker.Phone)
                .NotEmpty().WithMessage("The name cannot be empty.")
                .Matches(@"^\+\d{1,3}\s?\d{1,14}(\s?\d{1,13})?$") // regex for international numbers
                .WithMessage("Phone must be a valid international number.");
        }
    }
}
