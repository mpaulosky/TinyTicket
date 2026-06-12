using System.ComponentModel.DataAnnotations;

namespace TinyTicket.Web.Models;

public class TicketUpdateModel
{

	public int Id { get; set; }

	[Required, MinLength(1), MaxLength(50)]
	public string TicketTitle { get; set; } = string.Empty;

	[Required, MinLength(1), MaxLength(500)]
	public string TicketDescription { get; set; } = string.Empty;

	public DateTime? DateCompleted { get; set; }

	[Range(1, 5)]
	public int Priority { get; set; }

}
