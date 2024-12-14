using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Dtos.Assignment
{
    public class CreateAssignmentRequestDto
    {
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public int VolunteerId { get; set; }
    }
}