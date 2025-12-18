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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Configure the relationship between Ticket and Comment
		modelBuilder.Entity<TicketEntity>()
				.HasMany(t => t.Comments)
				.WithOne()
				.HasForeignKey(c => c.TicketId)
				.OnDelete(DeleteBehavior.Cascade);

		// Ensure table names are correct
		modelBuilder.Entity<TicketEntity>().ToTable("Tickets");
		modelBuilder.Entity<Comment>().ToTable("Comments");
	}
}