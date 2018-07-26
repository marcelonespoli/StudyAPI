using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Context;
using StudyAPI.Infra.Data.Entities;
using StudyAPI.Infra.Data.Repository.Interfaces;

namespace StudyAPI.Infra.Data.Repository.Repositories
{
    public class EmailSentRepository : GenericRepository<EmailSent>, IEmailSentRepository
    {
        public EmailSentRepository(StudyAPIContext context) 
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
