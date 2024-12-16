using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Dtos.VolunteerContract
{
    public class UpdateVolunteerContractDto
    {
        public int VolunteerId { get; set; }
        public int OrganizationId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}