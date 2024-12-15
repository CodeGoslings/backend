using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Models
{
    [Table("Volunteers")]
    public class Volunteer
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        public List<VolunteerContract> VolunteerContracts { get; set; } = new List<VolunteerContract>();

    }
}