using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class OrganisationCallback
    {
        [Key]
        public int OrgCallback_ID { get; set; }
        public int Organisation_ID { get; set; }
        public string Type_VC { get; set; }
        public string Endpoint_VC { get; set; }

    }
}
