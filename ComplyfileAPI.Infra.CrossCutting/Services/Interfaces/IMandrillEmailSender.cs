using System;
using System.Collections.Generic;
using System.Text;
using ComplyfileAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models;
using ComplyfileAPI.Infra.CrossCutting.Services.Email.MandrillEmail.Models.SenderData;

namespace ComplyfileAPI.Infra.CrossCutting.Services.Interfaces
{
    public interface IMandrillEmailSender
    {
        void SetMandrillEmailSender(string userName, string key);
        MandrillMessageInfo CheckStatus(string idMessage);
        MandrillMessageStatus Send(MandrillEmailMessage message);
    }
}
