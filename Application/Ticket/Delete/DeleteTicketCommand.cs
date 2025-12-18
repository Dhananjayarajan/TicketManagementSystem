using MediatR;

namespace TicketProject.Application.Ticket.Delete;

public record DeleteTicketCommand(int Id) : IRequest<bool>;