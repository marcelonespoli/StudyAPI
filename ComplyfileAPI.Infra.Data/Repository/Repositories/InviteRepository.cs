using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class InviteRepository : GenericRepository<Invite>, IInviteRepository
    {
        public InviteRepository(ComplyfileApiContext context) 
            : base(context)
        {
        }

        public bool AddInvite(Invite invite)
        {
            try
            {
                Insert(invite);
                Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
