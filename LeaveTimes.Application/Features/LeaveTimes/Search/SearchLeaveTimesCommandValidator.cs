namespace LeaveTimes.Application.Features.LeaveTimes.Search;

public sealed class SearchLeaveTimesCommandValidator : MyValidator<SearchLeaveTimesCommand>
{
    public SearchLeaveTimesCommandValidator()
    {
        var supportedReasons = string.Join(", ", Enum.GetValues<Reason>().Select(r => $"'{r}'"));

        RuleFor(x => x.Reason)
            .IsEnumName(typeof(Reason))
            .When(x => !string.IsNullOrEmpty(x.Reason))
            .WithMessage($"Reason must be one of the following: {supportedReasons}.");

        RuleFor(x => x.Year)
            .InclusiveBetween(2000, 2100)
            .When(x => x.Year.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Year must be between 2000 and 2100");

        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12)
            .When(x => x.Month.HasValue, ApplyConditionTo.CurrentValidator)
            .WithMessage("Month must be between 1 and 12");
    }
}
