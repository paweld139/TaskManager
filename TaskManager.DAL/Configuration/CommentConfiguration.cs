using System.Data.Entity.ModelConfiguration;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Configuration
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            HasRequired(c => c.Employee)
                .WithMany(e => e.Comments)
                .HasForeignKey(c => c.EmployeeId)
                .WillCascadeOnDelete(false);

            HasMany(c => c.Files)
                .WithOptional(f => f.Comment)
                .HasForeignKey(f => f.CommentId)
                .WillCascadeOnDelete(true);
        }
    }
}
