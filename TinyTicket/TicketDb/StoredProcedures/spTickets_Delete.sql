
create procedure dbo.spTickets_Delete
    @Id int
as
begin
    delete from dbo.Tickets
    where Id = @Id
end

GO

