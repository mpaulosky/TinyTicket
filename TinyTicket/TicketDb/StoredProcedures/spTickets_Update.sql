
create procedure dbo.spTickets_Update
    @Id int,
    @TicketTitle nvarchar(50),
    @TicketDescription nvarchar(max),
    @DateCompleted datetime2,
    @Priority int = 5

as
begin
    update dbo.Tickets
    set 
      TicketTitle = @TicketTitle,
      TicketDescription = @TicketDescription,
      DateCompleted = @DateCompleted,
      Priority = @Priority
    where Id = @Id
end

GO

