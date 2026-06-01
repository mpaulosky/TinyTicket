
create procedure dbo.spTickets_GetAll
as
begin
    select *
     from dbo.Tickets;
end

GO

