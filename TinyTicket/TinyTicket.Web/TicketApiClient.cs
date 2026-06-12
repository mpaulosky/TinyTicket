using System.Net;

using TinyTicket.Web.Models;

namespace TinyTicket.Web;

public sealed class TicketApiClient(HttpClient httpClient)
{

	private const string BaseRoute = "/api/tickets";

	public async Task<List<TicketModel>> GetTicketsAsync()
	{

		var tickets =
			await httpClient.GetFromJsonAsync<List<TicketModel>>(BaseRoute);

		return tickets ?? [];

	}

	public async Task<TicketModel?> GetTicketAsync(int id)
	{

		var response = await httpClient.GetAsync($"{BaseRoute}/{id}");

		if (response.StatusCode == HttpStatusCode.NotFound)
		{
			return null;
		}

		response.EnsureSuccessStatusCode();

		return await response.Content.ReadFromJsonAsync<TicketModel>();

	}

	public async Task CreateTicketAsync(TicketInsertModel ticket)
	{
		var response = await httpClient.PostAsJsonAsync(BaseRoute, ticket);

		response.EnsureSuccessStatusCode();

	}

	public async Task UpdateTicketAsync(TicketUpdateModel ticket)
	{

		var response = await httpClient.PutAsJsonAsync(BaseRoute, ticket);

		response.EnsureSuccessStatusCode();

	}

	public async Task DeleteTicketAsync(int id)
	{

		var response = await httpClient.DeleteAsync($"{BaseRoute}/{id}");

		response.EnsureSuccessStatusCode();

	}

}

