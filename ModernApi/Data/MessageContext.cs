namespace ModernApi.Data;

using Microsoft.EntityFrameworkCore;

public class MessageContext : DbContext
{
    public MessageContext(DbContextOptions<MessageContext> options)
        : base(options)
    {
    }

    public DbSet<Identity>? Identities { get; set; }
    public DbSet<Recipient>? Recipients { get; set; }
    public DbSet<Message>? Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // columns
        modelBuilder.Entity<Recipient>()
            .Property(r => r.RecipientTypeId)
            .HasConversion<short>();

        // relations
        modelBuilder.Entity<Message>()
            .HasOne(p => p.Sender)
            .WithMany(b => b.Messages)
            .HasForeignKey(p => p.SenderId);

        modelBuilder.Entity<Recipient>()
            .HasOne(p => p.Message)
            .WithMany(b => b.Recipients)
            .HasForeignKey(fk => fk.MessageId);
        modelBuilder.Entity<Recipient>()
            .HasOne(p => p.Identity)
            .WithMany(b => b.Recipients)
            .HasForeignKey(fk => fk.RecipientId);

        base.OnModelCreating(modelBuilder);
    }
}