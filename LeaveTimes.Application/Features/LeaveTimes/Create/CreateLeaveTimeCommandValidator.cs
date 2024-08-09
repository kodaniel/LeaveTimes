namespace LeaveTimes.Application.Features.LeaveTimes.Create;

public sealed class CreateLeaveTimeCommandValidator : MyValidator<CreateLeaveTimeCommand>
{
    public CreateLeaveTimeCommandValidator()
    {
        var supportedReasons = string.Join(", ", Enum.GetValues<Reason>().Select(r => $"'{r}'"));

        RuleFor(x => x.EmployeeName)
            .NotEmpty()
                .WithMessage("The name of employee is required.")
            .MaximumLength(100)
                .WithMessage("The name of the employee can be maximum of 100 characters.");

        RuleFor(x => x.Reason)
            .NotEmpty()
                .WithMessage("The reason can not be null.")
            .IsEnumName(typeof(Reason), caseSensitive: false)
                .WithMessage($"Reason must be one of the following: {supportedReasons}.");

        RuleFor(x => x.StartDate)
            .NotNull()
                .WithMessage("The start date can not be null.")
            .LessThanOrEqualTo(x => x.EndDate)
                .WithMessage("The start date must be less than or equal to the end date.");

        RuleFor(x => x.EndDate)
            .NotNull()
                .WithMessage("The end date can not be null.");

        RuleFor(x => x.Comment)
            .MaximumLength(500)
                .WithMessage("The comment can be maximum of 100 characters.");
    }
}