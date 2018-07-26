using System;
using System.Collections.Generic;
using System.Text;

namespace StudyAPI.Infra.CrossCutting.Services.Document
{
    public enum DocumentVisibility
    {
        Administrators = 1,
        VolunteerAndAdministrators = 2,
        Public = 3
    }
}
