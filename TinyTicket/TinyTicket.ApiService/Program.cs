using TinyTicket.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

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
		options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
	});
}

app.MapGet("/", () => "API service is running. Navigate to /weatherforecast to see sample data.");

//
app.MapGet("/api/tickets/{id:int}", async Task<IResult>
	(SqlDataAccess sql, int id) =>
{

	var tickets = await sql.LoadDataAsync<TicketModel, dynamic>
	(
		storedProcedure: "dbo.spTickets_Get",
		parameters: new { id },
		connectionStringName: "TicketDb"
	);

	var output = tickets.FirstOrDefault();

	return output is null ? TypedResults.NotFound() : TypedResults.Ok(output);

});

app.MapGet("/api/tickets", async (SqlDataAccess sql) =>
{

	var tickets = await sql.LoadDataAsync<TicketModel, dynamic>
	(
		storedProcedure: "dbo.spTickets_GetAll",
		parameters: new { },
		connectionStringName: "TicketDb"
	);

	return Results.Ok(tickets);

});

app.MapDefaultEndpoints();

app.Run();
