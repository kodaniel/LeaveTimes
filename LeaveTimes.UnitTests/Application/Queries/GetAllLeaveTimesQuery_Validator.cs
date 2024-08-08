using FluentValidation.TestHelper;
using LeaveTimes.Application.Queries;

namespace LeaveTimes.UnitTests.Application.Queries;

[TestFixture]
public class GetAllLeaveTimesQuery_Validator
{
    private GetAllLeaveTimesQuery.Validator validator;

    [SetUp]
    public void SetUp()
    {
        validator = new GetAllLeaveTimesQuery.Validator();
    }

    [Test]
    public void Should_not_have_error_When_request_is_empty()
    {
        var model = new GetAllLeaveTimesQuery.Request();

        var result = validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Test]
    public void Should_have_error_When_reason_is_invalid()
    {
        var model = new GetAllLeaveTimesQuery.Request { Reason = "invalid" };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Reason)
            .WithErrorMessage("Reason must be one of the following: 'Holiday', 'PaidLeave', 'NonPaidLeave', 'BusinessTravel', 'HomeOffice'.");
    }

    [Test]
    public void Should_have_error_When_year_is_small()
    {
        var model = new GetAllLeaveTimesQuery.Request { Year = -1 };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Year)
            .WithErrorMessage("Year must be between 2000 and 2100");
    }

    [TestCase(0)]
    [TestCase(13)]
    public void Should_have_error_When_month_is_outside_range(int month)
    {
        var model = new GetAllLeaveTimesQuery.Request { Month = month };

        var result = validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Month)
            .WithErrorMessage("Month must be between 1 and 12");
    }
}
