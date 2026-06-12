using System.ComponentModel.DataAnnotations;

namespace TinyTicket.Web.Models;

public class TicketInsertModel
{
	[Required, MinLength(1), MaxLength(50)]
	public string TicketTitle { get; set; } = string.Empty;

	[Required, MinLength(1), MaxLength(500)]
	public string TicketDescription { get; set; } = string.Empty;

	[Range(1, 5)]
	public int Priority { get; set; }
}
