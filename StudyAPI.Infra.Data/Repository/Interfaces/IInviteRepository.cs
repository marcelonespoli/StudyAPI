using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface IInviteRepository : IRepository<Invite>
    {
        bool AddInvite(Invite invite);
    }
}
