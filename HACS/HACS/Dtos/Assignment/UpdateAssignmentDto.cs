using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Models;

namespace HACS.Dtos.Assignment
{
    public class UpdateAssignmentDto
    {

        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public AssignmentStatus Status { get; set; }
        public int Rating { get; set; }
        public int VolunteerId { get; set; }
    }
}