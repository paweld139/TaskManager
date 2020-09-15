using System.Data.Entity.ModelConfiguration;
using TaskManager.BLL.Entities;

namespace TaskManager.DAL.Configuration
{
    public class TicketConfiguration : EntityTypeConfiguration<Ticket>
    {
        public TicketConfiguration()
        {
            HasRequired(t => t.Status)
                .WithMany(d => d.StatusTickets)
                .HasForeignKey(t => t.StatusId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Type)
                .WithMany(d => d.TypeTickets)
                .HasForeignKey(t => t.TypeId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Priority)
                .WithMany(d => d.PriorityTickets)
                .HasForeignKey(d => d.PriorityId)
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Contrahent)
                .WithMany(d => d.Tickets)
                .HasForeignKey(d => d.ContrahentId)
                .WillCascadeOnDelete(false);
        }
    }
}
