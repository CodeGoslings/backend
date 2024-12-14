using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public AssignmentStatus Status { get; set; } = AssignmentStatus.NotStarted;
        public int Rating { get; set; }
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; } = null!;
    }

    public enum AssignmentStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Cancelled
    }
}