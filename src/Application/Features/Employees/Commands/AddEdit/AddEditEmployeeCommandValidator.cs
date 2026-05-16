// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.RegularExpressions;

namespace CleanArchitecture.Blazor.Application.Features.Employees.Commands.AddEdit;

public class AddEditEmployeeCommandValidator : AbstractValidator<AddEditEmployeeCommand>
{
    public AddEditEmployeeCommandValidator()
    {

        RuleFor(v => v.Name)
                .MaximumLength(256)
                 .NotEmpty();
        RuleFor(v => v.PhoneNumber)
                .MaximumLength(256)
                 .NotEmpty();
        RuleFor(v => v.Email)
                .MaximumLength(256)
                .EmailAddress()
                .NotEmpty();
        RuleFor(v => v.PhoneNumber)
                      .NotEmpty()
                      .NotNull()
                      .MinimumLength(8)
                      .MaximumLength(16)
                      .Matches(new Regex(@"^\+[1-9]\d{1,14}$"));
        RuleFor(v=>v.DepartmentId)
              .NotNull();

    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AddEditEmployeeCommand>.CreateWithOptions((AddEditEmployeeCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

