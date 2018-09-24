using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Zupa.Models
{
    public class Seat
    {
        public int Id { get; set; }
        [Required]
        public int Row { get; set; }
        [Required]
        public int Col { get; set; }
        [Required]
        public string Email { get; set; }
        // Foreign Key
        public int MeetingId { get; set; }
        // Navigation property
        public Meeting Meeting { get; set; }
    }
}