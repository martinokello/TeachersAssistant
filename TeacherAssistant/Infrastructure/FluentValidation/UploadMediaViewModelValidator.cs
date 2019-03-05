using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeacherAssistant.Models;

namespace TeacherAssistant.Infrastructure.FluentValidation
{
    public class UploadMediaViewModelValidator:AbstractValidator<UploadMediaViewModel>
    {
        public UploadMediaViewModelValidator()
        {
            RuleFor(x => x.MediaId).GreaterThan(0).When(x=> !string.IsNullOrEmpty(x.RoleName) && x.SubjectId < 1 && !string.IsNullOrEmpty(x.MediaType) && !string.IsNullOrEmpty(x.Name) && x.MediaContent != null).WithMessage("Select Media Id");
            RuleFor(x => x.RoleName).NotEmpty().When(x=> x.MediaId < 1).WithMessage("Select Role Name");
            RuleFor(x => x.MediaType).NotEmpty().When(x => x.MediaId < 1).WithMessage("Select Media Type");
            RuleFor(x => x.MediaContent).NotNull().When(x => x.MediaId < 1).WithMessage("Pick Media");
            RuleFor(x => x.Name).NotEmpty().When(x => x.MediaId < 1).WithMessage("Name is required");
            RuleFor(x => x.SubjectId).GreaterThan(0).When(x => x.MediaId < 1).WithMessage("Pick a Subject");
        }
    }
}