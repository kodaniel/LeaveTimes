using FluentValidation.TestHelper;
using LeaveTimes.Application.Features.LeaveTimes.Search;

namespace LeaveTimes.UnitTests.Application.Features;

[TestFixture]
public class SearchLeaveTimesCommandValidatorTests
{
    private SearchLeaveTimesCommandValidator validator;

    [SetUp]
    public void SetUp()
    {
        validator = new SearchLeaveTimesCommandValidator();
    }

    [Test]
    public void Should_not_have_error_When_request_is_empty()
    {
        var model = new SearchLeaveTimesCommand(null, null, null, null);

        var result = validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void Should_not_have_error_When_request_is_valid()
    {
        var model = new SearchLeaveTimesCommand(2024, 8, "Jon Doe", "holiday");

        var result = validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void Should_have_error_When_reason_is_invalid()
    {
        var model = new SearchLeaveTimesCommand(null, null, null, Reason: "invalid");

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Reason)
            .WithErrorMessage("Reason must be one of the following: 'Holiday', 'PaidLeave', 'NonPaidLeave', 'BusinessTravel', 'HomeOffice'.");
    }

    [Test]
    public void Should_have_error_When_year_is_small()
    {
        var model = new SearchLeaveTimesCommand(Year: -1, null, null, null);

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Year)
            .WithErrorMessage("Year must be between 2000 and 2100");
    }

    [TestCase(0)]
    [TestCase(13)]
    public void Should_have_error_When_month_is_outside_range(int month)
    {
        var model = new SearchLeaveTimesCommand(null, month, null, null);

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Month)
            .WithErrorMessage("Month must be between 1 and 12");
    }
}
