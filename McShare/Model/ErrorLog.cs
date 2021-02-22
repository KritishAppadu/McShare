using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace McShare.Model
{
    public class ErrorLog
    {
        [Column("Error ID")]
        [Key]
        public int ErrorID { get; set; }

        [Column("Error Descrition")]
        public string ErrorDesc { get; set; }
    }
}
