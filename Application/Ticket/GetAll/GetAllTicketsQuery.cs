using System;
using MediatR;

namespace TicketProject.Application.Ticket.GetAll;

public record GetAllTicketsQuery : IRequest<List<TicketDto>>;

