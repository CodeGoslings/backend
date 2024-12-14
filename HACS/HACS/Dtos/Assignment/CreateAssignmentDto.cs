using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Dtos.Assignment
{
    public class CreateAssignmentDto
    {
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
    }
}