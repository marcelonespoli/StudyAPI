using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using StudyAPI.Infra.CrossCutting.Models;
using StudyAPI.Infra.Data.Entities;

namespace StudyAPI.Infra.CrossCutting.Services.Interfaces
{
    public interface IEmailFunctions
    {
        Task<bool> SendVolunteerInviteEmail(Volunteer vol, string token, int orgID, string currentUserEmail, List<KeyValuePair<string, Stream>> Attachments = null);
    }
}
