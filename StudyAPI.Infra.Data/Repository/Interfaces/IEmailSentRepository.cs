using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.Data.Repository.Interfaces
{
    public interface IEmailSentRepository : IRepository<EmailSent>
    {
        bool AddEmailSent(EmailSent emailSent);
    }
}
