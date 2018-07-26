using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyAPI.Infra.Data.Entities
{
    public class UpdatedEntityLog
    {
        [Key]
        public int UpdatedEntityLog_ID { get; set; }

        public int Entity_ID { get; set; }

        public string EntityType_VC { get; set; }

        public DateTime UpdatedDate_DT { get; set; }
    }
}
