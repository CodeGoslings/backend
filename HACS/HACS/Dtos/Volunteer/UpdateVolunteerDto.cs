using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Dtos.Assignment;

namespace HACS.Dtos.Volunteer
{
    public class UpdateVolunteerDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}