using System;
using Microsoft.EntityFrameworkCore;
using TicketProject.Domain;

namespace TicketProject.Persistance;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

	public DbSet<TicketEntity> Tickets => Set<TicketEntity>();
	public DbSet<Comment> Comments => Set<Comment>();
}
