using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Entities;

namespace ComplyfileAPI.Infra.Data.Repository.Interfaces
{
    public interface IInviteRepository : IRepository<Invite>
    {
        bool AddInvite(Invite invite);
    }
}
