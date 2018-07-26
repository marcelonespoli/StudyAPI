using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyfileAPI.Infra.Data.Entities
{
    public class Color
    {
        [Key]
        public int Color_ID { get; set; }

        [Display(Name = "Value")]
        public string Value_VC { get; set; }

    }
}
