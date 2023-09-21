using DbLocalizationSample;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DBLocalizationSample.Pages;

public class FormCreateCommand
{
    [Display(Name = "Firstname")]
    public string Firstname { get; set; }

    [Display(Name = "Lastname")]
    public string Lastname { get; set; }

    [Display(Name = "Email address")]
    public string Email { get; set; }
}

public class FormCreateCommandValidator : AbstractValidator<FormCreateCommand>
{
    public FormCreateCommandValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Firstname).NotEmpty().Length(5, 10).WithName(x => localizer[nameof(x.Firstname)]);
        RuleFor(x => x.Lastname).NotEmpty().Length(5, 10).WithName(x => localizer[nameof(x.Lastname)]);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithName(x => localizer[nameof(x.Email)]);
    }
}

public class FormModel : PageModel
{
    [BindProperty]
    public FormCreateCommand Command { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost([FromServices] IValidator<FormCreateCommand> validator)
    {
        var result = await validator.ValidateAsync(Command);

        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return Page();
        }

        return RedirectToPage("/Index");
    }
}
