using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Zupa.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        [Required]
        public string Date { get; set; }
    }
}
