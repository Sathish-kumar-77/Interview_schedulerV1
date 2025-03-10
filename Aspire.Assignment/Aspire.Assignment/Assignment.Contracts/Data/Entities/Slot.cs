using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Contracts.Data.Entities;

public class Slot
{
         [Key]
        public int SlotId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; }


}
