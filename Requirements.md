# API Endpoints

## GET /leave-times
Retrieve a list of registered leave times.

### Query parameters:
- ✅ year (optional): Filter by year.
- ✅ month (optional): Filter by month.
- ✅ employeeName (optional): Filter by employee name.
- ✅ reason (optional): Filter by reason. Possible values are "Holiday", "PaidLeave", "NonPaidLeave", "BusinessTravel", "HomeOffice".

✅ Default: Current year and month.

## POST /leave-times
Add a new leave time.

### Request body parameters:
- ✅ employeeName (string, max 100 characters): The name of the employee.
- ✅ startDate (string): The start date of the leave, should handle timezones.
- ✅ endDate (string): The end date of the leave, should handle timezones.
- ✅ reason (string): The reason for the leave. Possible values are "Holiday", "PaidLeave", "NonPaidLeave", "BusinessTravel", "HomeOffice".
- ✅ comment (string, optional, max 500 characters): A comment about the leave.

### Validations:
- ✅ The start date must be less than or equal to the end date.
- ✅ The reason must be one of the predefined values.
- ✅ The comment must be no longer than 500 characters.

## PUT /leave-times/\{id\}
Edit an existing leave time.

### Request body parameters:
- ✅ employeeName (string, max 100 characters): The name of the employee.
- ✅ startDate (string): The start date of the leave, should handle timezones.
- ✅ endDate (string): The end date of the leave, should handle timezones.
- ✅ reason (string): The reason for the leave. Possible values are "Holiday", "PaidLeave", "NonPaidLeave", "BusinessTravel", "HomeOffice".
- ✅ comment (string, optional, max 500 characters): A comment about the leave.

### Validations:
- ✅ The start date must be less than or equal to the end date.
- ✅ The reason must be one of the predefined values.
- ✅ The comment must be no longer than 500 characters.

## DELETE /leave-times/\{id\}
Delete a leave time by its ID.

> ✅ Note: Only leave times that have not been approved can be deleted.

# Technical Requirements
- ✅ Framework: Use ASP.NET 6+.
- ✅ Database: Utilize Entity Framework Core with either an in-memory database or SQLite for data storage.
- ✅ Validation: Ensure proper validation of inputs.
- ✅ Documentation: Provide API documentation using Swagger/OpenAPI.
- ✅ Libraries: Any library can be used as long as it is open source and free to use commercially.
- ✅ Code Quality: The API should be structured in a way that makes it easy to add new functionality in the future. While writing unit tests is not required, the code should be prepared to be unit testable.

# Submission
- Provide a link to the repository (GitHub, GitLab, etc.) containing your code.
- Include a README.md file with instructions on how to set up and run the project locally.
- Ensure your code is clean, well-documented, and follows best practices.