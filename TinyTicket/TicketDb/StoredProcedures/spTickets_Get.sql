
CREATE procedure [dbo].[spTickets_Get]
    @Id int
as
begin
    select *
    from dbo.Tickets
    where Id = @Id
end

GO

