using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Entities;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class EmailSentRepository : GenericRepository<EmailSent>, IEmailSentRepository
    {
        public EmailSentRepository(ComplyfileApiContext context) 
            : base(context)
        {
        }

        public bool AddEmailSent(EmailSent emailSent)
        {
            try
            {
                Insert(emailSent);
                Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
