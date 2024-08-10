using FluentValidation.Validators;

namespace LeaveTimes.Application.Validation.Validators;

public class DateTimeValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => nameof(DateTimeValidator<T, TProperty>);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value is DateTime)
            return true;

        string? strValue = value?.ToString();

        if (string.IsNullOrEmpty(strValue))
            return false;

        return DateTime.TryParse(strValue, out _);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "{PropertyName} must be a valid date format.";
    }
}
