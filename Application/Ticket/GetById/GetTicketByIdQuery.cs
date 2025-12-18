using MediatR;

namespace TicketProject.Application.Ticket.GetById;

public record GetTicketByIdQuery(int Id) : IRequest<TicketDetailsDto?>;