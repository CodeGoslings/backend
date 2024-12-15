using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Models
{
    [Table("VolunteerContracts")]
    public class VolunteerContract
    {
        public int VolunteerId { get; set; }
        public int OrganizationId { get; set; }
        public Volunteer Volunteer { get; set; }
        public Organization Organization { get; set; }
    }
}