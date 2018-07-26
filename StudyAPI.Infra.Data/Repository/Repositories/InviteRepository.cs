using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class InviteRepository : GenericRepository<Invite>, IInviteRepository
    {
        public InviteRepository(StudyAPIContext context) 
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
