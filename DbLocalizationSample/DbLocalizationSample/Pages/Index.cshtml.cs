using DbLocalizationSample;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DBLocalizationSample.Pages;

public class FormCreateCommand
{
    [Display(Name = "Firstname")]
    public string Firstname { get; set; }

    [Display(Name = "Lastname")]
    public string Lastname { get; set; }

    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Email address")]
    public string Email { get; set; }

    [Display(Name = "Nationality")]
    public string Nationality { get; set; }

    [Display(Name = "Profession")]
    public string Profession { get; set; }

    [Display(Name = "Country of Residence")]
    public string CountryOfResidence { get; set; }

    [Display(Name = "Address")]
    public string Address { get; set; }

    [Display(Name = "Are you considering taking up residence in the Principality?")]
    public string Residency { get; set; }

    [Display(Name = "Spoken language(s)")]
    public List<string> Languages { get; set; } = new List<string>();

    [Display(Name = "How do you know CFM Indosuez Wealth Management?")]
    public List<string> KnowCfm { get; set; } = new List<string>();

    [Display(Name = "What are your expectations ?")]
    public List<string> Expectations { get; set; } = new List<string>();

    [Display(Name = "What are your needs ?")]
    public List<string> Needs { get; set; } = new List<string>();
}

public class FormCreateCommandValidator : AbstractValidator<FormCreateCommand>
{
    public FormCreateCommandValidator(IStringLocalizer localizer)
    {
        RuleFor(x => x.Firstname).NotEmpty().Length(5, 30).WithName(x => localizer[nameof(x.Firstname)]);
        RuleFor(x => x.Lastname).NotEmpty().Length(5, 30).WithName(x => localizer[nameof(x.Lastname)]);
        RuleFor(x => x.PhoneNumber).NotEmpty().Length(5, 30).WithName(x => localizer[nameof(x.PhoneNumber)]);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithName(x => localizer[nameof(x.Email)]);
        RuleFor(x => x.Nationality).NotEmpty().WithName(x => localizer[nameof(x.Nationality)]);
        RuleFor(x => x.Profession).NotEmpty().Length(5, 30).WithName(x => localizer[nameof(x.Profession)]);
        RuleFor(x => x.CountryOfResidence).NotEmpty().WithName(x => localizer[nameof(x.CountryOfResidence)]);
        RuleFor(x => x.Address).NotEmpty().Length(5, 30).WithName(x => localizer[nameof(x.Address)]);
        RuleFor(x => x.Residency).NotEmpty().WithName(x => localizer[nameof(x.Residency)]);
        RuleFor(x => x.Languages).Must(x => x.Count > 0).WithName(x => localizer[nameof(x.Languages)]);
        RuleFor(x => x.KnowCfm).Must(x => x.Count > 0).WithName(x => localizer[nameof(x.KnowCfm)]);
        RuleFor(x => x.Expectations).Must(x => x.Count > 0).WithName(x => localizer[nameof(x.Expectations)]);
        RuleFor(x => x.Needs).Must(x => x.Count > 0).WithName(x => localizer[nameof(x.Needs)]);
    }
}

public class IndexModel : PageModel
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

    public IActionResult OnGetSetCultureCookie(string cltr, string returnUrl)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return LocalRedirect(returnUrl);
    }

    public IActionResult OnPostSetCultureCookie(string cltr)
    {
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        return Page();
    }
}
