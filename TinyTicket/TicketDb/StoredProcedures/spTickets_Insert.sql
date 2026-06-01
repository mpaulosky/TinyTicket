
create procedure dbo.spTickets_Insert
    @TicketTitle nvarchar(50),
    @TicketDescription nvarchar(max),
    @Priority int = 5

as
begin
    insert into dbo.Tickets (TicketTitle, TicketDescription, Priority)
    values (@TicketTitle, @TicketDescription, @Priority)
end

GO

