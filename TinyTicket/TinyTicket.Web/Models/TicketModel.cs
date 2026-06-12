namespace TinyTicket.Web.Models;

public class TicketModel
{

	public int Id { get; set; }
	public string? TicketTitle { get; set; } = string.Empty;
	public string? TicketDescription { get; set; } = string.Empty;
	public DateTime? DateCreated { get; set; }
	public DateTime? DateCompleted { get; set; }
	public int Priority { get; set; }

}
