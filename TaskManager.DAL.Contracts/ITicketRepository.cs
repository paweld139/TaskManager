﻿using PDCore.Repositories.IRepo;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using TaskManager.BLL.Entities;
using TaskManager.BLL.Entities.Basic;
using TaskManager.BLL.Entities.Details;

namespace TaskManager.DAL.Contracts
{
    public interface ITicketRepository : ISqlRepositoryEntityFrameworkDisconnected<Ticket>
    {
        IQueryable<Ticket> Find(bool includeSubobjects, bool asNoTracking = true);

        Task<Ticket> FindByIdAsync(int id, bool includeSubobjects, bool asNoTracking = true);

        Task<Ticket> FindByNumberAsync(string number, bool includeSubobjects);

        Task<TicketDetails> FindDetailsByNumberAsync(string number);
    }
}
