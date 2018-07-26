using System;
using System.Collections.Generic;
using System.Text;
using StudyAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models;
using StudyAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models.SenderData;

namespace StudyAPI.Infra.CrossCutting.Services.Interfaces
{
    public interface IMandrillEmailSender
    {
        void SetMandrillEmailSender(string userName, string key);
        MandrillMessageInfo CheckStatus(string idMessage);
        MandrillMessageStatus Send(MandrillEmailMessage message);
    }
}
