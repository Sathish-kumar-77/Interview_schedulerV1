using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Contracts.DTO;

public class SlotDetailsDTO
{

        public int SlotId { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }

}
