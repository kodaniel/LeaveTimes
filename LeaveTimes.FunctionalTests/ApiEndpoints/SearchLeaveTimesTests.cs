using LeaveTimes.Application.Features;
using LeaveTimes.Application.Features.LeaveTimes.Search;
using LeaveTimes.Infrastructure.Middlewares;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LeaveTimes.FunctionalTests.ApiEndpoints;

[TestFixture]
public class SearchLeaveTimesTests : ApiFunctionalTests
{
    private JsonSerializerOptions _options;

    public SearchLeaveTimesTests()
    {
        _options = new JsonSerializerOptions();
        _options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        _options.Converters.Add(new JsonStringEnumConverter());
    }

    [Test]
    public async Task Should_ReturnHttp200_When_No_Filters()
    {
        var inputUrl = "/leave-times";

        var result = await _client.GetAsync(inputUrl);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Should_Return_Filtered_When_Set()
    {
        var inputUrl = "/leave-times?year=2024&month=8&reason=Holiday";

        var result = await _client.GetAsync(inputUrl);
        var content = await result.Content.ReadFromJsonAsync<ListResponse<LeaveTimeResponse>>(_options);

        Assert.That(content, Is.Not.Null);
        Assert.That(content.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task Should_ReturnHttp404_When_Invalid_Query()
    {
        var inputUrl = "/leave-times?year=0&month=-1&reason=aaa";

        var result = await _client.GetAsync(inputUrl);
        var content = await result.Content.ReadFromJsonAsync<ExceptionDetails>(_options);

        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(content, Is.Not.Null);
        Assert.That(content.Title, Is.EqualTo("Validation Failed"));
        Assert.That(content.Detail, Is.EqualTo("One or More Validations failed"));
        Assert.That(content.Errors, Is.Not.Null);
        Assert.That(content.Errors.Count, Is.EqualTo(3));
        Assert.That(content.Errors[0], Is.EqualTo("Reason must be one of the following: 'Holiday', 'PaidLeave', 'NonPaidLeave', 'BusinessTravel', 'HomeOffice'."));
        Assert.That(content.Errors[1], Is.EqualTo("Year must be between 2000 and 2100"));
        Assert.That(content.Errors[2], Is.EqualTo("Month must be between 1 and 12"));
    }
}
