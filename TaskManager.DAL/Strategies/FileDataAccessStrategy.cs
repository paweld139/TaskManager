using PDCore.Interfaces;
using PDCore.Utils;
using PDCoreNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Strategies
{
    public class FileDataAccessStrategy : TaskManagerDataAccessStrategy<File>
    {
        public FileDataAccessStrategy(IPrincipal principal) : base(principal)
        {
        }

        public override Task<bool> CanAdd(params object[] args)
        {
            return Task.FromResult(HasRole);
        }

        public override bool CanDelete(File entity)
        {
            return EmployeeId == entity.UserId || NoRestrictions(); //Plik może usunąć jedynie osoba, która go dodała
        }

        public override bool CanUpdate(File entity)
        {
            throw new NotImplementedException();
        }

        public override ICollection<string> GetPropertiesForUpdate(File entity)
        {
            throw new NotImplementedException();
        }

        public override void PrepareForAdd(params object[] args)
        {
            if (args[2] is FileModel file && args[1] is ObjType type && args[0] is IEntity<int> parent)
            {
                string[] fileSegments = file.Source.Split(',');

                file.Data = Convert.FromBase64String(fileSegments[1]);

                file.Source = string.Empty;

                if (string.IsNullOrWhiteSpace(file.MimeType))
                {
                    file.MimeType = fileSegments[0].Split(';')[0].Split(':')[1];
                }

                file.Extension = IOUtils.GetSimpleExtension(file.Name);

                file.Name = file.Name.Split('.')[0];

                file.RefGid = type;

                file.RefId = parent.Id;

                file.UserId = EmployeeId;
            }
        }

        public override IQueryable<File> PrepareQuery(IQueryable<File> entities)
        {
            if(IsCustomer)
            {
                entities = entities.Where(f => (f.TicketId != null && f.Ticket.ContrahentId == ContrahentId) ||
                                               (f.CommentId != null && f.Comment.Ticket.ContrahentId == ContrahentId));
            }

            return entities;
        }
    }
}
