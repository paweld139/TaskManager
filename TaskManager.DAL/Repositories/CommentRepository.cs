using AutoMapper;
using PDCore.Interfaces;
using PDCoreNew.Context.IContext;
using PDCoreNew.Repositories.Repo;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Repositories
{
    public class CommentRepository : SqlRepositoryEntityFrameworkDisconnected<Comment>, ICommentRepository
    {
        public CommentRepository(IEntityFrameworkDbContext ctx, ILogger logger, IMapper mapper) : base(ctx, logger, mapper)
        {
        }

        public IQueryable<Comment> Find(bool includeFiles, bool asNoTracking = true)
        {
            if (!includeFiles)
            {
                return FindAll(asNoTracking);
            }

            return FindAll(asNoTracking)
                    .Include(c => c.Files);
        }

        public Task<Comment> FindByIdAsync(int id, bool includeFiles, bool asNoTracking = true)
        {
            return Find(includeFiles, asNoTracking).SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
