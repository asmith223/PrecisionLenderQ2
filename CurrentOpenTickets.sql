WITH OpenStatuses AS (
	SELECT TicketId, NewStatus 
FROM StatusChange S 
WHERE Timestamp=(
SELECT MAX(timestamp) 
FROM StatusChange 
WHERE TicketId = S.TicketId) and (S.NewStatus = 'In Progress' or S.NewStatus = 'Reopened')
)
Select Id, Summary, OpenStatuses.NewStatus as 'Status'
FROM Ticket,OpenStatuses
WHERE Ticket.Id = OpenStatuses.TicketId
