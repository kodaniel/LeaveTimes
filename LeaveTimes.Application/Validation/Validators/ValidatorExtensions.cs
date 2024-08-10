namespace LeaveTimes.Application.Validation.Validators;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, TProperty> IsValidDateTime<T, TProperty>(this IRuleBuilder<T, TProperty> rule)
    {
        return rule.SetValidator(new DateTimeValidator<T, TProperty>());
    }
}
