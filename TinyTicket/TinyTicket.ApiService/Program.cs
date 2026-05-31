using System.ComponentModel.DataAnnotations;

using TinyTicket.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Add validation services.
builder.Services.AddValidation();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Database access service.
builder.Services.AddSingleton<SqlDataAccess>();

// Build the app.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/openapi/v1.json", "TinyTicket App API V1");
	});
}

// Root endpoint to confirm the API is running.

app.MapGet("/", () => "API service is running. Navigate to /swagger to work with the API.");

// Get a ticket by ID.

app.MapGet("/api/tickets/{id:int}", async Task<IResult>
	(SqlDataAccess sql, int id) =>
{

	var tickets = await sql.LoadDataAsync<TicketModel, dynamic>
	(
		storedProcedure: "dbo.spTickets_Get",
		parameters: new { Id = id },
		connectionStringName: "TicketDb"
	);

	var output = tickets.FirstOrDefault();

	return output is null ? TypedResults.NotFound() : TypedResults.Ok(output);

});

// Get all tickets.

app.MapGet("/api/tickets", async Task<IResult>
	(SqlDataAccess sql) =>
{

	var tickets = await sql.LoadDataAsync<TicketModel, dynamic>
	(
		storedProcedure: "dbo.spTickets_GetAll",
		parameters: new { },
		connectionStringName: "TicketDb"
	);

	return Results.Ok(tickets);

});

// Insert a new ticket.

app.MapPost("/api/tickets", async Task<IResult>
	(SqlDataAccess sql, TicketInsertRecord ticket) =>
{

	await sql.SaveDataAsync
	(
		storedProcedure: "dbo.spTickets_Insert",
		parameters: ticket,
		connectionStringName: "TicketDb"
	);

	return TypedResults.NoContent();

});

// Update an existing ticket.

app.MapPut("/api/tickets", async Task<IResult>
	(SqlDataAccess sql, TicketUpdateRecord ticket) =>
{

	await sql.SaveDataAsync
	(
		storedProcedure: "dbo.spTickets_Update",
		parameters: ticket,
		connectionStringName: "TicketDb"
	);

	return TypedResults.NoContent();

});

// Delete a ticket by ID.

app.MapDelete("/api/tickets/{id:int}", async Task<IResult>
	(SqlDataAccess sql, int id) =>
{

	await sql.SaveDataAsync
	(
		storedProcedure: "dbo.spTickets_Delete",
		parameters: new { Id = id },
		connectionStringName: "TicketDb"
	);

	return TypedResults.NoContent();

});

app.MapDefaultEndpoints();

app.Run();

public record TicketInsertRecord
(
	[Required, MinLength(1), MaxLength(50)] string TicketTitle,
	[Required, MinLength(1), MaxLength(500)] string TicketDescription,
	[Range(1, 5)] int Priority
);

public record TicketUpdateRecord
(
	[Required, Range(1, int.MaxValue)] int Id,
	[Required, MinLength(1), MaxLength(50)] string TicketTitle,
	[Required, MinLength(1), MaxLength(500)] string TicketDescription,
	DateTime? DateCompleted,
	[Range(1, 5)] int Priority
);
